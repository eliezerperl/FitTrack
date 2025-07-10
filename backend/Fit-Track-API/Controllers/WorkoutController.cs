using Fit_Track_API.Models.API_DTOs.Workout;
using Fit_Track_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fit_Track_API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class WorkoutController : ControllerBase {

		private readonly IWorkoutService _workoutService;

		public WorkoutController(IWorkoutService workoutService) {
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
		}

		[HttpGet("image/{exerciseId}")]
		public async Task<IActionResult> GetExerciseImage(string exerciseId) {
			var (imageData, contentType) = await _workoutService.GetExerciseImageAsync(exerciseId);
			return File(imageData, contentType);
		}



		[HttpGet("equipment/{equipment}")]
		public async Task<IActionResult> GetEquipmentWorkouts(string equipment) {
			List<ExerciseDto> equipmentExcercises = await _workoutService.GetEquipmentWorkoutsAsync(equipment);

			return Ok(equipmentExcercises);
		}

		[HttpGet("equipmentList")]
		public async Task<IActionResult> GetEquipmentList() {
			List<string> equipmentList = await _workoutService.GetEquipmentListAsync();

			return Ok(equipmentList);
		}


		[HttpGet("bodyPart/{bodyPart}")]
		public async Task<IActionResult> GetBodyPartWorkouts(string bodyPart) {
			List<ExerciseDto> bodyPartExcercises = await _workoutService.GetBodyPartWorkoutsAsync(bodyPart);

			return Ok(bodyPartExcercises);
		}

		[HttpGet("bodyPartList")]
		public async Task<IActionResult> GeBodyPartList() {
			List<string> bodyPartList = await _workoutService.GetBodyPartListAsync();

			return Ok(bodyPartList);
		}


		[HttpGet("muscle/{muscle}")]
		public async Task<IActionResult> GetMuscleWorkouts(string muscle) {
			List<ExerciseDto> muscleExcercises = await _workoutService.GetTargetMuscleWorkoutsAsync(muscle);

			return Ok(muscleExcercises);
		}


		[HttpGet("muscleList")]
		public async Task<IActionResult> GetMuscleList() {
			List<string> muscleList = await _workoutService.GetTargetMuscleListAsync();

			return Ok(muscleList);
		}
	}
}
