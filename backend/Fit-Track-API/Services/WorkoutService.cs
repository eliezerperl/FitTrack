using Fit_Track_API.External;
using Fit_Track_API.Models.API_DTOs.Workout;
using Fit_Track_API.Models.DTOs;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.InterFaces;
using Fit_Track_API.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fit_Track_API.Services {
	public class WorkoutService : IWorkoutService {

		private readonly WorkoutApiClient _workoutApiClient;
		private readonly IWorkoutRepository _workoutRepo;

		public WorkoutService(WorkoutApiClient workoutApiClient, IWorkoutRepository workoutRepository) {
			_workoutApiClient = workoutApiClient ?? throw new ArgumentNullException(nameof(workoutApiClient));
			_workoutRepo = workoutRepository;
		}
		// Exception throwing in Repository Layer
		// Create
		public async Task<WorkoutEntry> CreateAsync(WorkoutEntry workoutEntryDto, Guid userId, User user) {
			WorkoutEntry workoutEntry = new WorkoutEntry {
				UserId = userId,
				User = user, // Set the User navigational property
				ExerciseId = workoutEntryDto.ExerciseId,
				ExerciseName = workoutEntryDto.ExerciseName,
				Date = workoutEntryDto.Date,
				Sets = workoutEntryDto.Sets,
				Reps = workoutEntryDto.Reps,
				Weight = workoutEntryDto.Weight,
				Duration = workoutEntryDto.Duration,
				Notes = workoutEntryDto.Notes
			};

			await _workoutRepo.CreateAsync(workoutEntry);

			return workoutEntry;
		} 

		// Get All
		public async Task<IEnumerable<WorkoutEntry>> GetAllAsync() {
			return await _workoutRepo.GetAllAsync();
		}

		// Get By Workout Id
		public async Task<WorkoutEntry> GetByIdAsync(Guid id) {
			WorkoutEntry workoutEntry = await _workoutRepo.GetByIdAsync(id);

			return workoutEntry;
		}

		// Get By User Id
		public async Task<IEnumerable<WorkoutEntry>> GetByUserIdAsync(Guid userId) {
			IEnumerable<WorkoutEntry> userWorkouts = await _workoutRepo.GetByUserIdAsync(userId);

			return userWorkouts;
		}

		// Update
		public async Task<WorkoutEntry> UpdateAsync(Guid id, WorkoutEntry workoutEntryDto) {
			WorkoutEntry workoutEntry = await _workoutRepo.GetByIdAsync(id);
				workoutEntry.Sets = workoutEntryDto.Sets;
				workoutEntry.Reps = workoutEntryDto.Reps;
				workoutEntry.Weight = workoutEntryDto.Weight;
				workoutEntry.Duration = workoutEntryDto.Duration;
				workoutEntry.Notes = workoutEntryDto.Notes;
			await _workoutRepo.UpdateAsync(id, workoutEntry);
			return workoutEntry;
		}

		// Delete
		public async Task DeleteAsync(Guid id) {
			await _workoutRepo.DeleteAsync(id);
		}




		// Third party API consumption
		public async Task<(byte[] ImageData, string ContentType)> GetExerciseImageAsync(string exerciseId) {
			var image = await _workoutApiClient.GetExerciseImageAsync(exerciseId);

			if (image.ImageData == null || image.ContentType == null) {
				throw new NullReferenceException("The image data or content type is null.");
			}

			return (image.ImageData, image.ContentType);
		}


		public async Task<List<string>> GetTargetMuscleListAsync() {
			string listAsString = await _workoutApiClient.GetTargetMuscleListAsync();

			return serializeStringAndReturnStringList(listAsString);
		}

		public async Task<List<ExerciseDto>> GetTargetMuscleWorkoutsAsync(string muscle) {
			string musclExercises = await _workoutApiClient.GetTargetMuscleWorkoutsAsync(muscle);

			return CapitalizeFromStringAndReturnExerciseList(musclExercises);
		}


		public async Task<List<string>> GetBodyPartListAsync() {
			string listAsString = await _workoutApiClient.GetBodyPartListAsync();

			return serializeStringAndReturnStringList(listAsString);
		}

		public async Task<List<ExerciseDto>> GetBodyPartWorkoutsAsync(string bodyPart) {
			string bodyPartExercises = await _workoutApiClient.GetBodyPartWorkoutsAsync(bodyPart);

			return CapitalizeFromStringAndReturnExerciseList(bodyPartExercises);
		}



		public async Task<List<string>> GetEquipmentListAsync() {
			string equipmentList = await _workoutApiClient.GetEquipmentListAsync();

			return serializeStringAndReturnStringList(equipmentList);

		}

		public async Task<List<ExerciseDto>> GetEquipmentWorkoutsAsync(string equipment) {
			string equipmentExercises = await _workoutApiClient.GetEquipmentWorkoutsAsync(equipment);

			return CapitalizeFromStringAndReturnExerciseList(equipmentExercises);
		}


		//Helper Method to return string list (avoid redundancy)
		private List<string> serializeStringAndReturnStringList(string listAsString) {
			List<string> list = JsonConvert.DeserializeObject<List<string>>(listAsString);

			if (list == null) {
				throw new NullReferenceException("The list is null.");
			}
			if (list.Count == 0) {
				throw new ArgumentException("The list is empty.");
			}

			return list;
		}

		//Helper Methods to capatalize and return ExerciseDto list (avoid redundancy)
		private List<ExerciseDto> CapitalizeFromStringAndReturnExerciseList(string exercisesAsString) {
			List<ExerciseDto> exercises = JsonConvert.DeserializeObject<List<ExerciseDto>>(exercisesAsString);

			if (exercises == null) {
				throw new NullReferenceException("The exercises list is null.");
			}
			if (exercises.Count == 0) {
				throw new ArgumentException("The exercises list is empty.");
			}

			foreach (var ex in exercises) {
				ex.Name = CapitalizeEachWord(ex.Name);
			}

			return exercises;
		}
		private string CapitalizeEachWord(string input) {
			if (string.IsNullOrWhiteSpace(input)) return input;

			return string.Join(" ", input
				.Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
		}


	}
}
