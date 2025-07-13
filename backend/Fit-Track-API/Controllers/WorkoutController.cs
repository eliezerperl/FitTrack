using Fit_Track_API.Models.API_DTOs.Workout;
using Fit_Track_API.Models.DTOs;
using Fit_Track_API.Models.Entities;
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
		//WORKOUT ENTRY CRUD OPERATIONS
		[HttpPost]
		public async Task<IActionResult> CreateWorkoutEntry([FromBody] WorkoutEntry workoutEntry) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var userId = Guid.NewGuid(); // This should be replaced with the actual user ID from the authenticated user context
			var user = new User { Id = userId }; // This should be replaced with the actual user object from the authenticated user context
			var createdEntry = await _workoutService.CreateAsync(workoutEntry, userId, user);
			return CreatedAtAction(nameof(GetAllWorkouts), new { id = createdEntry.Id }, createdEntry);
		}

		[HttpGet]
		public async Task <IActionResult> GetAllWorkouts() {
			var allWorkouts = await _workoutService.GetAllAsync();

			return Ok(allWorkouts);
		}

		//By Workout id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetWorkoutById(Guid id) {
			var workout = await _workoutService.GetByIdAsync(id);
			if (workout == null) {
				return NotFound($"Workout with ID {id} not found.");
			}
			return Ok(workout);
		}

		//[HttpGet]
		//[Route("user/{userId}")]
		//public async Task<IActionResult> GetWorkoutsByUserId(Guid userId) {
		//	var userWorkouts = await _workoutService.GetByUserIdAsync(userId);
		//	if (userWorkouts == null || !userWorkouts.Any()) {
		//		return NotFound($"No workouts found for user with ID {userId}.");
		//	}
		//	return Ok(userWorkouts);
		//}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateWorkout(Guid id, [FromBody] WorkoutEntry workoutEntry) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var updatedWorkout = await _workoutService.UpdateAsync(id, workoutEntry);
			if (updatedWorkout == null) {
				return NotFound($"Workout with ID {id} not found.");
			}
			return Ok(updatedWorkout);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteWorkout(Guid id) {
			await _workoutService.DeleteAsync(id);
			return NoContent();
		}





		//THIRD PARTY API CALLS
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
