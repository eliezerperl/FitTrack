using System.ComponentModel.DataAnnotations;

namespace Fit_Track_API.Models.Entities {
	public class WorkoutEntry : IEntity {
		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid UserId { get; set; } // I set this in contoller from request data
		public User User { get; set; } // Navigational Property

		[Required(ErrorMessage = "ExerciseId is required")]
		public string ExerciseId { get; set; }

		[Required(ErrorMessage = "ExerciseName is required")]
		public string ExerciseName { get; set; }

		[Required(ErrorMessage = "Date is required")]
		public DateTime Date { get; set; }

		public int Sets { get; set; }
		public int Reps { get; set; }
		public double? Weight { get; set; }
		public TimeSpan? Duration { get; set; }

		public string? Notes { get; set; }
	}
}
