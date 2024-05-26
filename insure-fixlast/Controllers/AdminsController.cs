using Microsoft.AspNetCore.Mvc;

namespace insure_fixlast.Controllers
{
	public class AdminsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
