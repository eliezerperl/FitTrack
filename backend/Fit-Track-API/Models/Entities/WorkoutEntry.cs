namespace Fit_Track_API.Models.Entities {
	public class WorkoutEntry : IEntity {
		public int Id { get; set; }
		public string UserId { get; set; }
		public string ExerciseName { get; set; }
		public int DurationMinutes { get; set; }
		public int CaloriesBurned { get; set; }
		public DateTime Date { get; set; }
	}
}
