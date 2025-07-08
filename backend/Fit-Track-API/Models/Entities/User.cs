using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;

namespace Fit_Track_API.Models.Entities {
	public class User : IEntity {

		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

	}
}
