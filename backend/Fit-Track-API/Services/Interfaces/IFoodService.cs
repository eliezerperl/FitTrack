using Fit_Track_API.Models.API_DTOs;
using Fit_Track_API.Models.Entities;

namespace Fit_Track_API.Services.Interfaces {
	public interface IFoodService: IServiceBase<FoodEntry> {
		Task<List<FoodDto>> GetFoodsByNameAsync(string foodName);
		
		Task<List<Nutrient>> GetFoodNutrientsByNameAsync(string foodName);


	}
}
