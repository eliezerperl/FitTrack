using System.ComponentModel.DataAnnotations;

namespace Fit_Track_API.Models.Entities {
	public class FoodEntry : IEntity {
		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid UserId { get; set; } // Set in controller via request
		public User User { get; set; } // Navigational Property


		[Required]
		public string FoodName { get; set; }

		public DateTime DateLogged { get; set; } = DateTime.UtcNow;

		public List<Nutrient> Nutrients { get; set; } = new();

		public string? Notes { get; set; }
	}
}
