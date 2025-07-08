using Fit_Track_API.Models.DTOs.Auth;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories;
using Fit_Track_API.Repositories.InterFaces;
using Fit_Track_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fit_Track_API.Services {
	public class AuthService : IAuthService {

		private readonly IAuthRepository _authRepository;

		public AuthService(IAuthRepository authRepository) {
			_authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
		}

		public async Task<string> LoginPipeAsync(AuthRequestDto authRequestDto) {
			AuthResponseDto res = await _authRepository.LoginAsync(authRequestDto);

			if (!res.IsSuccess) {
				throw ReturnException(res.Exception.ToString(), res.ExceptionMessage);
			}

			return res.Token;
		}

		public async Task<User> RegisterPipeAsync(AuthRequestDto authRequestDto) {
			AuthResponseDto res = await _authRepository.RegisterAsync(authRequestDto);

			if (!res.IsSuccess) {
				throw ReturnException(res.Exception.ToString(), res.ExceptionMessage);
			}

			return res.User;
		}

		private Exception ReturnException(string exceptionType, string message) { 
			switch (exceptionType) {
				case "NullReference":
					throw new NullReferenceException(message);
				case "Argument":
					throw new ArgumentException(message);
				case "UnauthorizedAccess":
					throw new UnauthorizedAccessException(message);
				default:
					throw new Exception("An unknown error occurred.");
			}
		}
	}
}
