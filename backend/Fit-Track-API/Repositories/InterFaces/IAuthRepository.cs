using Fit_Track_API.Models.DTOs.Auth;
using Fit_Track_API.Models.Entities;
using System.Data;

namespace Fit_Track_API.Repositories.InterFaces {
	public interface IAuthRepository {

		Task<AuthResponseDto> RegisterAsync(AuthRequestDto request);

		Task<AuthResponseDto> LoginAsync(AuthRequestDto request);
	}
}
