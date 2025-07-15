using System.ComponentModel.DataAnnotations;

namespace Fit_Track_API.Models.Entities {
	public class Nutrient {
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public string Name { get; set; }

		[Required]
		public string Unit { get; set; }

		[Required]
		public double Value { get; set; }

	}
}
