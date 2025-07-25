﻿using Fit_Track_API.Models.DTOs.Auth;
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

			Response.Cookies.Append("jwtToken", token, new CookieOptions
			{
				Expires = DateTime.UtcNow.AddMinutes(60),
				HttpOnly = true,
				SameSite = SameSiteMode.None,
				Secure = true, 
			});

			return Ok(new { Token = token });
		}

		[HttpPost("logout")]
		public IActionResult Logout() {
			Response.Cookies.Delete("jwtToken", new CookieOptions
			{
				HttpOnly = true,
				SameSite = SameSiteMode.None,
				Secure = true,
			});
			return NoContent(); // 204
		}
	}
}
