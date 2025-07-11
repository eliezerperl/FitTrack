using Fit_Track_API.Models.DTOs;
using Fit_Track_API.Models.Entities;

namespace Fit_Track_API.Services.Interfaces {
	public interface IServiceBase<T> where T: class {
		//CRUD Operations
		// Create
		Task<T> CreateAsync(T Dto, Guid userId);


		// Get All
		Task<IEnumerable<T>> GetAllAsync();

		// Get By Workout Id
		Task<T> GetByIdAsync(Guid id);

		// Get By User Id (soon)
		// Update
		Task<T> UpdateAsync(Guid id, T Dto);

		// Delete
		Task DeleteAsync(Guid id);
	}
}
