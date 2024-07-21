using CalorieCounter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            // get the user from the cookie

            // get the weight history by user id

            ProfileViewModel profile = new();

            return View(profile);
        }
    }
}
