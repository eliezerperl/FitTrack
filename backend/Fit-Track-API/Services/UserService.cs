using AutoMapper;
using Fit_Track_API.Models.DTOs;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.Interfaces;
using Fit_Track_API.Services.Interfaces;

namespace Fit_Track_API.Services {
	public class UserService : IUserService {
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository) {
			_userRepository = userRepository;
		}

		public async Task<User> CreateAsync(UserEntityDto userDto) {
			if (userDto == null) {
				throw new ArgumentNullException(nameof(userDto), "User DTO cannot be null.");
			}
			var user = new User {
				UserName = userDto.UserName,
				UserId = userDto.UserId,
			};
			await _userRepository.CreateAsync(user);
			return user;
		}

		public Task<User> CreateAsync(User Dto, Guid userId, User user) {
			throw new NotImplementedException();
		}

		public Task DeleteAsync(Guid id) {
			throw new NotImplementedException();
		}

		public Task<IEnumerable<User>> GetAllAsync() {
			throw new NotImplementedException();
		}

		public async Task<User> GetByIdAsync(Guid id) {
			var user = await _userRepository.GetByIdAsync(id);

			return user;
		}

		public Task<IEnumerable<User>> GetByUserIdAsync(Guid userId) {
			throw new NotImplementedException();
		}

		public Task<User> UpdateAsync(Guid id, User Dto) {
			throw new NotImplementedException();
		}
	}
}
