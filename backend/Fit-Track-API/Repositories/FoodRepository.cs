using Fit_Track_API.Data;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.InterFaces;

namespace Fit_Track_API.Repositories {
	public class FoodRepository: RepositoryBase<FoodEntry>, IFoodRepository {

		public FoodRepository(FitTrackDbContext context) : base(context) {
		}
	}
}
