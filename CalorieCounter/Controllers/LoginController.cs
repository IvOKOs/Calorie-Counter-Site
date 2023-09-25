using CalorieCounter.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalorieCounter.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDatabaseData _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IDatabaseData db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor; 
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserModel passedUser) 
        {
            DataLibrary.Models.UserModel userModel = _db.GetUserByEmailAndPass(passedUser.Email, passedUser.Password); 
            
            if(userModel != null)
            {
                if(passedUser.Email == userModel.Email && passedUser.Password == userModel.Password)
                {
                    // create claim for authenticated user 
                    var claims = new List<Claim>
                    {
                        new Claim("UserId", userModel.Id.ToString())
                    }; 

                    // create authentication ticket 
                    var identity = new ClaimsIdentity(claims, "CustomAuthentication"); 
                    var principal = new ClaimsPrincipal(identity);
                    var authenticationProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(1) 
                    }; 

                    // Sign in the user by creating the authentication cookie
                    await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authenticationProperties); 

                    return RedirectToAction("Index", "Home", new {userModel.Id});  
                }
            }

            bool isError = true;
            return View(isError);  
        }
    }
}
