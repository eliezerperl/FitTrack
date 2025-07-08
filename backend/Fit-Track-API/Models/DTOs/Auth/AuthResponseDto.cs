using Fit_Track_API.Models.Entities;
using Fit_Track_API.Models.Enums;

namespace Fit_Track_API.Models.DTOs.Auth {
	public class AuthResponseDto {
		public bool IsSuccess { get; set; }
		public string? Token { get; set; }
		public User? User { get; set; }
		public ExceptionEnum? Exception { get; set; }
		public string? ExceptionMessage { get; set; }
	}
}
