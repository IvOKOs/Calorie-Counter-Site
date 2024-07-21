using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
