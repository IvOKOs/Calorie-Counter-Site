using CalorieCounter.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalorieCounter.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IDatabaseData _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterController(IDatabaseData db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db; 
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: Register
        //public ActionResult Index()
        //{
        //    return View();
        //} 


        // GET: Register/Create
        public ActionResult Create()
        {
            return View(); 
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check to see if there is an existing email in db 
                    string enteredEmail = user.Email.Trim();

                    int id = _db.GetUserId(enteredEmail); 

                    if(id != 0)
                    {
                        return RedirectToAction("Index", "AccountExistsError", new { user.Email });
                    } 
 
                    _db.InsertUser(user.FirstName,
                                   user.LastName,
                                   user.Email,
                                   user.Password,
                                   user.Gender,
                                   user.Age,
                                   user.Height,
                                   user.Weight,
                                   user.Activity, 
                                   user.Goal); 
 
                    id = _db.GetUserId(enteredEmail);

                    //add claims
                    var claims = new List<Claim>
                        {
                            new Claim("UserId", id.ToString())
                        };

                    // create authentication ticket 
                    var identity = new ClaimsIdentity(claims, "CustomAuthentication");
                    var principal = new ClaimsPrincipal(identity);
                    var authenticationProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = null,
                        IsPersistent = false,
                    };

                    // Sign in the user by creating the authentication cookie
                    await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authenticationProperties);

                    return RedirectToAction("Index", "Home", new { id }); 
                }
                return View(); 
            }
            catch
            {
                return View();
            }
        }
    }
}
