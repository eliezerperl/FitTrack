using Fit_Track_API.Models.DTOs.Auth;
using Fit_Track_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fit_Track_API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase {

		private readonly IAuthService _authService;

		public AuthController(IAuthService authService) {
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] AuthRequestDto authRequestDto) {
			var user = await _authService.RegisterPipeAsync(authRequestDto);

			return Ok(user);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] AuthRequestDto authRequestDto) {
			var token = await _authService.LoginPipeAsync(authRequestDto);

			return Ok(token);
		}
	}
}
