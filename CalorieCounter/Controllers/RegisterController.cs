using CalorieCounter.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IDatabaseData _db;

        public RegisterController(IDatabaseData db)
        {
            _db = db; 
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
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check to see if there is an existing email in db 
                    string enteredEmail = user.Email.Trim(); 
                    bool emailExists = false; 

                    int id = _db.GetUserId(enteredEmail); 

                    if(id != 0)
                    {
                        emailExists = true; 
                    }
                    
                    if (emailExists)
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

                    // create auth cookie !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 

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
