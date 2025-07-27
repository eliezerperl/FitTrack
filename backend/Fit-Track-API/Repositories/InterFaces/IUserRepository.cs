using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.InterFaces;

namespace Fit_Track_API.Repositories.Interfaces {
	public interface IUserRepository: IRepositoryBase<User> {

		//Task<User> GetByUserNameAsync(string userName);
	}
}
