using Fit_Track_API.Data;
using Fit_Track_API.Models.Entities;
using Fit_Track_API.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Fit_Track_API.Repositories {
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity {

		private readonly FitTrackDbContext _dbContext;
		protected readonly DbSet<T> _dbSet;

		public RepositoryBase(FitTrackDbContext context) {
			_dbContext = context ?? throw new ArgumentNullException(nameof(context));
			_dbSet = _dbContext.Set<T>() ?? throw new ArgumentNullException(nameof(_dbSet));
		}

		public async Task CreateAsync(T entity) {
			_dbSet.Add(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id) {
			var deletingEntity = await _dbSet.FindAsync(id);
			if (deletingEntity == null)
				throw new ArgumentException($"Entity with ID {id} does not exist.");

			_dbSet.Remove(deletingEntity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync() {
			var listOfEntites = await _dbSet.ToListAsync();

			return listOfEntites;
		}

		public async Task<T> GetByIdAsync(Guid id) {
			var wantedEntity = await _dbSet.FindAsync(id);
			if (wantedEntity == null)
				throw new ArgumentException($"Entity with ID {id} does not exist.");

			return wantedEntity;
		}

		public async Task<IEnumerable<T>> GetByUserIdAsync(Guid userId) {
			var entities = await _dbSet.Where(e => e.UserId == userId).ToListAsync();
			if (entities == null || !entities.Any())
				throw new ArgumentException($"No entities found for user with ID {userId}.");
			return entities;
		}

		public async Task UpdateAsync(Guid id, T entity) {
			var existingEntity = await _dbSet.FindAsync(id);
			if (existingEntity == null)
				throw new ArgumentException($"Entity with ID {id} does not exist.");

			_dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

			await _dbContext.SaveChangesAsync();
		}

	}
}
