using Fit_Track_API.Models.Entities;

namespace Fit_Track_API.Models.DTOs {
	public class UserEntityDto : IEntity {
		public Guid UserId { get; set; }

		public string UserName { get; set; }

		public ICollection<WorkoutEntry> WorkoutEntries { get; set; }

		public ICollection<FoodEntry> FoodEntries { get; set; }
	}
}
