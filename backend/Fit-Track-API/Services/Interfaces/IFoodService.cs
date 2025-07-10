using Fit_Track_API.Models.API_DTOs;

namespace Fit_Track_API.Services.Interfaces {
	public interface IFoodService {
		Task<List<FoodDto>> GetFoodsByNameAsync(string foodName);
	}
}
