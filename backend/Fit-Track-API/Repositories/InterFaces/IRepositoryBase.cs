using Fit_Track_API.Models.Entities;

namespace Fit_Track_API.Repositories.InterFaces {
	public interface IRepositoryBase<T> where T : class, IEntity {

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync(Guid id);

		Task<IEnumerable<T>> GetByUserIdAsync(Guid userId);

		Task CreateAsync(T entity);

		Task UpdateAsync(Guid id, T entity);

		Task DeleteAsync(Guid id);

	}
}
