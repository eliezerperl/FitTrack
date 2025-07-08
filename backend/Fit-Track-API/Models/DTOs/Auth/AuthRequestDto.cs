using System.ComponentModel.DataAnnotations;

namespace Fit_Track_API.Models.DTOs.Auth {
	public class AuthRequestDto {

		[Required(ErrorMessage = "Username is required.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }

	}
}
