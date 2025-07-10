using Fit_Track_API.External;
using Fit_Track_API.Models.API_DTOs;
using Fit_Track_API.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fit_Track_API.Services {
	public class FoodService : IFoodService {

		private readonly FoodApiClient _foodApiClient;

		public FoodService(FoodApiClient foodApiClient) {
			_foodApiClient = foodApiClient ?? throw new ArgumentNullException(nameof(foodApiClient));
		}

		public async Task<List<FoodDto>> GetFoodsByNameAsync(string foodName) {
			string foodData = await _foodApiClient.GetFoodsDataAsync(foodName);

			if (foodData == null || !foodData.Any()) {
				throw new ArgumentException($"No food found with the name: {foodName}");
			}

			var response = JsonConvert.DeserializeObject<FoodSearchResponse>(foodData);
			List<FoodDto> foods = response?.Foods ?? new List<FoodDto>();

			return foods;
		}
	}
}
