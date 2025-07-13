using Fit_Track_API.External;
using Fit_Track_API.Models.API_DTOs;
using Fit_Track_API.Models.DTOs;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.InterFaces;
using Fit_Track_API.Services.Interfaces;
using Newtonsoft.Json;

namespace Fit_Track_API.Services {
	public class FoodService : IFoodService {

		private readonly FoodApiClient _foodApiClient;
		private readonly IFoodRepository _foodRepo;

		public FoodService(FoodApiClient foodApiClient, IFoodRepository foodRepository) {
			_foodApiClient = foodApiClient ?? throw new ArgumentNullException(nameof(foodApiClient));
			_foodRepo = foodRepository;
		}

		// Create a new food entry
		public async Task<FoodEntry> CreateAsync(FoodEntry foodEntryDto, Guid userId, User user) {
			FoodEntry foodEntry = new FoodEntry {
				UserId = userId,
				User = user, // Set the User navigational property
				FoodName = foodEntryDto.FoodName,
				Nutrients = foodEntryDto.Nutrients,
				Notes = foodEntryDto.Notes
			};
			await _foodRepo.CreateAsync(foodEntry);
			return foodEntry;

		}

		// Get all food entries
		public async Task<IEnumerable<FoodEntry>> GetAllAsync() {
			return await _foodRepo.GetAllAsync();
		}

		// Get food entry by ID
		public async Task<FoodEntry> GetByIdAsync(Guid id) {
			FoodEntry foodEntry = await _foodRepo.GetByIdAsync(id);
			if (foodEntry == null) {
				throw new NullReferenceException($"Food entry with ID {id} does not exist.");
			}
			return foodEntry;
		}

		// Get food entries by user ID
		//public async Task<IEnumerable<FoodEntry>> GetByUserIdAsync(Guid userId) {
		//	List<FoodEntry> foodEntries = await _foodRepo.GetByUserIdAsync(userId);
		//	if (foodEntries == null) {
		//		throw new NullReferenceException($"No food entries found for user with ID {userId}.");
		//	}
		//	if (foodEntries.Count == 0) {
		//		throw new ArgumentException($"No food entries found for user with ID {userId}.");
		//	}

		//	return foodEntries;
		//}

		// Update a food entry
		public async Task<FoodEntry> UpdateAsync(Guid id, FoodEntry foodEntryDto) {
			FoodEntry foodEntry = await _foodRepo.GetByIdAsync(id);
			foodEntry.Notes = foodEntryDto.Notes;

			await _foodRepo.UpdateAsync(id, foodEntry);
			return foodEntry;
		}

		// Delete a food entry
		public async Task DeleteAsync(Guid id) {
			await _foodRepo.DeleteAsync(id);
		}



		// THIRD PARTY FOOD API
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
