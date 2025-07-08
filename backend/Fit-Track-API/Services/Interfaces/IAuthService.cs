using Fit_Track_API.Models.DTOs.Auth;
using Fit_Track_API.Models.Entities;

namespace Fit_Track_API.Services.Interfaces {
	public interface IAuthService {

		Task<string> LoginPipeAsync(AuthRequestDto authRequestDto);

		Task<User> RegisterPipeAsync(AuthRequestDto authRequestDto);
	}
}
