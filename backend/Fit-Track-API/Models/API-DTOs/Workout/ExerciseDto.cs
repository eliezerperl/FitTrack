﻿namespace Fit_Track_API.Models.API_DTOs.Workout {
	public class ExerciseDto {
		public string Id { get; set; }
		public string Name { get; set; }
		public string BodyPart { get; set; }
		public string Target { get; set; }
		public string Equipment { get; set; }
		public string Description { get; set; }
		public string Difficulty { get; set; }
		public string Category { get; set; }

		public List<string> SecondaryMuscles { get; set; }
		public List<string> Instructions { get; set; }
	}
}
