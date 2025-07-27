using Fit_Track_API.Data;
using Fit_Track_API.Models.DTOs.Auth;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.InterFaces;
using Fit_Track_API.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fit_Track_API.Repositories {
	public class AuthRepository : IAuthRepository {

		private readonly IConfiguration _config;
		private readonly FitTrackDbContext _dbContext;

		public AuthRepository(FitTrackDbContext context, IConfiguration config) {
			_config = config;
			_dbContext = context;
		}

		public async Task<AuthResponseDto> LoginAsync(AuthRequestDto request) {
			var user = await _dbContext.Users.SingleOrDefaultAsync(user => user.UserName == request.Username);
			if (user == null) {
				return new AuthResponseDto { IsSuccess = false, Exception = ExceptionEnum.NullReference, ExceptionMessage = "User not found." };
			}
			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) {
				return new AuthResponseDto { IsSuccess = false, Exception = ExceptionEnum.Argument, ExceptionMessage = "Wrong password." };
			}
			string token = CreateToken(user);
			return new AuthResponseDto { IsSuccess = true, Token = token };
		}

		public async Task<AuthResponseDto> RegisterAsync(AuthRequestDto request) {
			//if username exists, throw exception, I dont allow duplicates
			if (await _dbContext.Users.AnyAsync(user => user.UserName == request.Username)) {
				return new AuthResponseDto { IsSuccess = false, Exception = ExceptionEnum.Argument, ExceptionMessage = "Username already exists." };
			}

			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
			var newUser = new User();
			newUser.UserName = request.Username;
			newUser.PasswordHash = passwordHash;
			newUser.PasswordSalt = passwordSalt;
			_dbContext.Users.Add(newUser);
			await _dbContext.SaveChangesAsync();

			return new AuthResponseDto { IsSuccess = true, User = newUser };
		}

		string CreateToken(User user) {

			List<Claim> claims = new List<Claim>
			{
				new Claim("id", user.UserId.ToString()),
				new Claim("username", user.UserName),
				//new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
			var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
			using (var hmac = new HMACSHA512()) {
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
			using (var hmac = new HMACSHA512(passwordSalt)) {
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}
	}
}
