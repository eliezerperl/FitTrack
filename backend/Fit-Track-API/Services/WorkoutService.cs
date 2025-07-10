using Fit_Track_API.External;
using Fit_Track_API.Models.API_DTOs.Workout;
using Fit_Track_API.Services.Interfaces;
using Newtonsoft.Json;

namespace Fit_Track_API.Services {
	public class WorkoutService : IWorkoutService {

		private readonly WorkoutApiClient _workoutApiClient;

		public WorkoutService(WorkoutApiClient workoutApiClient) {
			_workoutApiClient = workoutApiClient ?? throw new ArgumentNullException(nameof(workoutApiClient));
		}

		public async Task<(byte[] ImageData, string ContentType)> GetExerciseImageAsync(string exerciseId) {
			return await _workoutApiClient.GetExerciseImageAsync(exerciseId);
		}


		public async Task<List<string>> GetTargetMuscleListAsync() {
			string listAsString = await _workoutApiClient.GetTargetMuscleListAsync();

			return JsonConvert.DeserializeObject<List<string>>(listAsString);
		}

		public async Task<List<ExerciseDto>> GetTargetMuscleWorkoutsAsync(string muscle) {
			string musclExercises = await _workoutApiClient.GetTargetMuscleWorkoutsAsync(muscle);

			return JsonConvert.DeserializeObject<List<ExerciseDto>>(musclExercises);
		}


		public async Task<List<string>> GetBodyPartListAsync() {
			string listAsString = await _workoutApiClient.GetBodyPartListAsync();

			return JsonConvert.DeserializeObject<List<string>>(listAsString);
		}

		public async Task<List<ExerciseDto>> GetBodyPartWorkoutsAsync(string bodyPart) {
			string bodyPartExercises = await _workoutApiClient.GetBodyPartWorkoutsAsync(bodyPart);

			return JsonConvert.DeserializeObject<List<ExerciseDto>>(bodyPartExercises);
		}



		public async Task<List<string>> GetEquipmentListAsync() {
			string equipmentList = await _workoutApiClient.GetEquipmentListAsync();

			return JsonConvert.DeserializeObject<List<string>>(equipmentList);
		}

		public async Task<List<ExerciseDto>> GetEquipmentWorkoutsAsync(string equipment) {
			string equipmentExercises = await _workoutApiClient.GetEquipmentWorkoutsAsync(equipment);

			return JsonConvert.DeserializeObject<List<ExerciseDto>>(equipmentExercises);
		}
	}
}
