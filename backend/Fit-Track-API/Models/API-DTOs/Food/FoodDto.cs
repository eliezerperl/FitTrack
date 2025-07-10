namespace Fit_Track_API.Models.API_DTOs {
	public class FoodDto {
		public int FdcId { get; set; }
		public string Description { get; set; }
		public string CommonNames { get; set; }
		public string AdditionalDescriptions { get; set; }
		public string DataType { get; set; }
		public long FoodCode { get; set; }
		public DateTime PublishedDate { get; set; }
		public string FoodCategory { get; set; }
		public int FoodCategoryId { get; set; }
		public string AllHighlightFields { get; set; }
		public double Score { get; set; }

		public List<object> Microbes { get; set; }
		public List<FoodNutrientDto> FoodNutrients { get; set; }
		public List<object> FinalFoodInputFoods { get; set; }
		public List<object> FoodMeasures { get; set; }
		public List<object> FoodAttributes { get; set; }
		public List<object> FoodAttributeTypes { get; set; }
		public List<object> FoodVersionIds { get; set; }
	}
}
