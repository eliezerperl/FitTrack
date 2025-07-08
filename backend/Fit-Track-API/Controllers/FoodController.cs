using Microsoft.AspNetCore.Mvc;

namespace Fit_Track_API.Controllers {
	public class FoodController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}
