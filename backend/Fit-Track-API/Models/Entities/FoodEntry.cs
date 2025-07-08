namespace Fit_Track_API.Models.Entities {
	public class FoodEntry : IEntity {
		public int Id { get; set; }
		public string UserId { get; set; }
		public string FoodName { get; set; }
		public int Calories { get; set; }
		public DateTime Date { get; set; }
	}
}
