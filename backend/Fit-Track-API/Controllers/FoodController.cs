using Fit_Track_API.Models.Entities;
using Fit_Track_API.Services;
using Fit_Track_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fit_Track_API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class FoodController : ControllerBase {

		private readonly IFoodService _foodService;

		public FoodController(IFoodService foodService) {
			_foodService = foodService ?? throw new ArgumentNullException(nameof(foodService));
		}
		// FOOD ENTRY CRUD OPERATIONS
		[HttpPost]
		public async Task<IActionResult> CreateFoodEntry([FromBody] FoodEntry foodEntry) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var userId = Guid.NewGuid(); // This should be replaced with the actual user ID from the authenticated user context
			var user = new User { Id = userId }; // This should be replaced with the actual user object from the authenticated user context
			var createdEntry = await _foodService.CreateAsync(foodEntry, userId, user);
			return CreatedAtAction(nameof(GetAllFoodEntries), new { id = createdEntry.Id }, createdEntry);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFoodEntries() {
			var allWorkouts = await _foodService.GetAllAsync();

			return Ok(allWorkouts);
		}

		//By foodentry id
		[HttpGet("find/{id}")]
		public async Task<IActionResult> GetFoodEntryById(Guid id) {
			var foodEntry = await _foodService.GetByIdAsync(id);
			if (foodEntry == null) {
				return NotFound($"Food Entry with ID {id} not found.");
			}
			return Ok(foodEntry);
		}

		//[HttpGet]
		//[Route("user/{userId}")]
		//public async Task<IActionResult> GetFoodEntriesByUserId(Guid userId) {
		//	var userFoodEnteries = await _foodService.GetByUserIdAsync(userId);
		//	if (userFoodEnteries == null || !userFoodEnteries.Any()) {
		//		return NotFound($"No Food Enteries found for user with ID {userId}.");
		//	}
		//	return Ok(userFoodEnteries);
		//}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateFoodEntry(Guid id, [FromBody] FoodEntry foodEntry) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			var updatedWorkout = await _foodService.UpdateAsync(id, foodEntry);
			if (updatedWorkout == null) {
				return NotFound($"Workout with ID {id} not found.");
			}
			return Ok(updatedWorkout);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFoodEntry(Guid id) {
			await _foodService.DeleteAsync(id);
			return NoContent();
		}



		// THIRD PARTY API OPERATIONS
		[HttpGet("{food}")]
		public async Task<IActionResult> GetFoodsByName(string food) {
			var foodData = await _foodService.GetFoodsByNameAsync(food);

			if (foodData == null || !foodData.Any()) {
				return NotFound($"No food data found for '{food}'.");
			}

			return Ok(foodData);
		}
	}
}
