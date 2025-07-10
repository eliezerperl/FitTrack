using Fit_Track_API.Models.API_DTOs.Workout;

namespace Fit_Track_API.Services.Interfaces {
	public interface IWorkoutService {
		Task<List<string>> GetTargetMuscleListAsync();

		Task<List<ExerciseDto>> GetTargetMuscleWorkoutsAsync(string muscle);

		Task<List<string>> GetBodyPartListAsync();

		Task<List<ExerciseDto>> GetBodyPartWorkoutsAsync(string bodyPart);

		Task<List<string>> GetEquipmentListAsync();

		Task<List<ExerciseDto>> GetEquipmentWorkoutsAsync(string equipment);

		Task<(byte[] ImageData, string ContentType)> GetExerciseImageAsync(string exerciseId);

	}
}
