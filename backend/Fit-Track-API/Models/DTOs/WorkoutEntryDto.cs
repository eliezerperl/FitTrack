using System.ComponentModel.DataAnnotations;

namespace Fit_Track_API.Models.DTOs {
	public class WorkoutEntryDto {
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
		public double? DistanceKm { get; set; }

		public string? Notes { get; set; }  
	}
}
