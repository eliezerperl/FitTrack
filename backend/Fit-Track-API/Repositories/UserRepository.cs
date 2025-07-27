using Fit_Track_API.Data;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.Interfaces;

namespace Fit_Track_API.Repositories {
	public class UserRepository: RepositoryBase<User>, IUserRepository {

		public UserRepository(FitTrackDbContext context) : base(context) {
		}
		//public async Task<User> GetByUserNameAsync(string userName) {
		//	var user = await _dbSet.Where(u => u.UserName == userName).ToListAsync();
		//	if (user == null)
		//		throw new ArgumentException($"User with username {userName} does not exist.");
		//	return user;
		//}
	}
}
