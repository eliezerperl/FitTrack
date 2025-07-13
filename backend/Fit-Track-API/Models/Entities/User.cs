using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;

namespace Fit_Track_API.Models.Entities {
	public class User {

		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public ICollection<WorkoutEntry> WorkoutEntries { get; set; } = new List<WorkoutEntry>();

		public ICollection<FoodEntry> FoodEntries { get; set; } = new List<FoodEntry>();

	}
}
