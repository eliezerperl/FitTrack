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

		[HttpGet("{food}")]
		public async Task<IActionResult> GetFoodsByName(string food) {
			var foodData = await _foodService.GetFoodsByNameAsync(food);

			return Ok(foodData);
		}
	}
}
