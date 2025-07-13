using System.ComponentModel.DataAnnotations;

namespace Fit_Track_API.Models.Entities {
	public class Nutrient {
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public string Name { get; set; }

		[Required]
		public string Unit { get; set; }  // e.g., "g", "mg", "kcal"

		[Required]
		public double Value { get; set; }

		public Guid FoodEntryId { get; set; }
		public FoodEntry FoodEntry { get; set; }
	}
}
