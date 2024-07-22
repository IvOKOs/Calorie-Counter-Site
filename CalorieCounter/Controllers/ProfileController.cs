using CalorieCounter.Models.ViewModels;
using DataLibrary.BusinessLogic;
using DataAccessModels = DataLibrary.Models;
using PresentationModels = CalorieCounter.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IDatabaseData _db;
        public ProfileController(IDatabaseData db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // get the user from the cookie
            int id = 0;
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim != null)
            {
                id = int.Parse(userIdClaim.Value);
                if(id == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            DataAccessModels.UserModel user = new();
            user = _db.GetUserById(id);

            // get the weight history by user id
            List<DataAccessModels.WeightHistoryModel> weightHistory = _db.GetWeightHistoryByUserId(user.Id);
            if(weightHistory == null)
            {
                weightHistory = new List<DataAccessModels.WeightHistoryModel>();
            }

            PresentationModels.UserModel userRes = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Activity = user.Activity,
                Goal = user.Goal,
            };

            List<PresentationModels.WeightHistoryModel> weightHistoryRes = new List<PresentationModels.WeightHistoryModel>();
            foreach(var weight in weightHistory)
            {
                PresentationModels.WeightHistoryModel weighRes = new()
                {
                    Weight = weight.Weight,
                    RecordedDate = weight.RecordedDate,
                };
                weightHistoryRes.Add(weighRes);
            }

            ProfileViewModel profile = new()
            {
                User = userRes,
                Weights = weightHistoryRes,
            };

            return View(profile);
        }
    }
}
