using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Controllers
{
    public class AccountExistsErrorController : Controller
    {
        // GET: AccountExistsErrorController
        public ActionResult Index(string email)
        {
            return View(email); 
        }
    }
}
