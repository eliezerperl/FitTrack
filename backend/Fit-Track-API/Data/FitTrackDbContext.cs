using Microsoft.EntityFrameworkCore;
using Fit_Track_API.Models.Entities;

namespace Fit_Track_API.Data {
	public class FitTrackDbContext : DbContext {
		public FitTrackDbContext(DbContextOptions<FitTrackDbContext> options)
			: base(options) {
		}

		public DbSet<User> Users { get; set; }
		public DbSet<FoodEntry> FoodEntries { get; set; }
		public DbSet<WorkoutEntry> WorkoutEntries { get; set; }
		public DbSet<ErrorLog> ErrorLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.UserName)
				.IsUnique();
		}
	}
}