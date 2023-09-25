using PresentationModels = CalorieCounter.Models;
using DataLibrary.BusinessLogic;
using DataAccessModels = DataLibrary.Models; 
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace CalorieCounter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseData _db;

        public HomeController(ILogger<HomeController> logger, IDatabaseData db)
        {
            _logger = logger;
            _db = db; 
        }

        public IActionResult Index(int id) 
        {
            DataAccessModels.UserModel user = new DataAccessModels.UserModel(); 
            
            if (id == 0)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                
                if (userIdClaim != null)
                {
                    id = int.Parse(userIdClaim.Value); 
                }
            }

            user = _db.GetUserById(id);

            double TDEE = 0;
            double caloriesGoal;
            double proteinGoal;
            double carbsGoal;
            double fatGoal; 

            if(user.Gender == "male")
            {
                TDEE = (9.99 * user.Weight) + (6.25 * user.Height) - (4.92 * user.Age) + 5; 
            }
            else if(user.Gender == "female")
            {
                TDEE = (9.99 * user.Weight) + (6.25 * user.Height) - (4.92 * user.Age) - 161; 
            }

            switch (user.Activity)
            {
                case 1:
                    TDEE *= 1.2;
                    break;
                case 2:
                    TDEE *= 1.375;
                    break;
                case 3:
                    TDEE *= 1.55;
                    break;
                case 4:
                    TDEE *= 1.725;
                    break;
                case 5:
                    TDEE *= 1.9;
                    break; 
            } 

            if(user.Goal == 1)
            {
                caloriesGoal = TDEE - 200;
                proteinGoal = (caloriesGoal * 0.35) / 4;
                carbsGoal = (caloriesGoal * 0.4) / 4; 
                fatGoal = (caloriesGoal * 0.25) / 9;  
                
            }
            else if(user.Goal == 2)
            {
                caloriesGoal = TDEE;
                proteinGoal = (caloriesGoal * 0.3) / 4;
                carbsGoal = (caloriesGoal * 0.45) / 4;
                fatGoal = (caloriesGoal * 0.25) / 9; 
            }
            else
            {
                caloriesGoal = TDEE + 200;
                proteinGoal = (caloriesGoal * 0.25) / 4;
                carbsGoal = (caloriesGoal * 0.5) / 4;
                fatGoal = (caloriesGoal * 0.25) / 9; 
            }
 
            List<int> userIds = _db.GetUserIdCountByUserId(id); 
            if(userIds.Count == 0)
            {
                // insert to MacroGoals
                _db.InsertMacroGoal(id, caloriesGoal, proteinGoal, carbsGoal, fatGoal);

                // get macroId based on userId 
                int macroGoalId = _db.GetMacroGoalByUserId(id);

                // insert to Calories
                _db.InsertInitialCalories(id, macroGoalId); 
            }
            
            string fullName = user.FirstName + " " + user.LastName;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            fullName = textInfo.ToTitleCase(fullName); 

            PresentationModels.UserGoalsViewModel userGoals = new()
            {
                FullName = fullName,
                Goal = user.GoalType.ToLower(), 
                CaloriesGoal = caloriesGoal,
                ProteinGoal = proteinGoal,
                CarbsGoal = carbsGoal,
                FatGoal = fatGoal
            };

            List<DataAccessModels.FoodModel> totalFoodsLib = _db.GetAllMealsByUser(id); 
            PresentationModels.TotalDailyMealsModel totals = new(); 

            foreach (DataAccessModels.FoodModel food in totalFoodsLib)
            {
                totals.TotalCalories += food.Calories;
                totals.TotalProtein += food.Protein;
                totals.TotalCarbs += food.Carbs;
                totals.TotalFat += food.Fat; 
            }

            double totalCalories = totals.TotalCalories; 
            double totalProtein = totals.TotalProtein; 
            double totalCarbs = totals.TotalCarbs; 
            double totalFat = totals.TotalFat; 

            // update Calories with these totals values 
            _db.UpdateTotalAmounts(id, totalCalories, totalProtein, totalCarbs, totalFat);  

            PresentationModels.ViewModels.HomeViewModel homeViewModel = new()
            {
                UserGoals = userGoals,
                Totals = totals
            }; 

            return View(homeViewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int goal) 
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            int id = 0; 

            if (userIdClaim != null)
            {
                id = int.Parse(userIdClaim.Value);
            }

            var user = _db.GetUserById(id);

            double TDEE = 0;
            double caloriesGoal = 0;
            double proteinGoal = 0;
            double carbsGoal = 0;
            double fatGoal = 0;

            if (user.Gender == "male")
            {
                TDEE = (9.99 * user.Weight) + (6.25 * user.Height) - (4.92 * user.Age) + 5;
            }
            else if (user.Gender == "female")
            {
                TDEE = (9.99 * user.Weight) + (6.25 * user.Height) - (4.92 * user.Age) - 161;
            }

            switch (user.Activity)
            {
                case 1:
                    TDEE *= 1.2;
                    break;
                case 2:
                    TDEE *= 1.375;
                    break;
                case 3:
                    TDEE *= 1.55;
                    break;
                case 4:
                    TDEE *= 1.725;
                    break;
                case 5:
                    TDEE *= 1.9;
                    break;
            }

            string goalType = ""; 

            if (goal == 1)
            {
                caloriesGoal = TDEE - 200;
                proteinGoal = (caloriesGoal * 0.35) / 4;
                carbsGoal = (caloriesGoal * 0.4) / 4;
                fatGoal = (caloriesGoal * 0.25) / 9;
                goalType = "lose weight"; 

            }
            else if (goal == 2)
            {
                caloriesGoal = TDEE;
                proteinGoal = (caloriesGoal * 0.3) / 4;
                carbsGoal = (caloriesGoal * 0.45) / 4;
                fatGoal = (caloriesGoal * 0.25) / 9;
                goalType = "maintain weight"; 
            }
            else if(goal == 3) 
            {
                caloriesGoal = TDEE + 200;
                proteinGoal = (caloriesGoal * 0.25) / 4;
                carbsGoal = (caloriesGoal * 0.5) / 4;
                fatGoal = (caloriesGoal * 0.25) / 9;
                goalType = "gain weight"; 
            }

            _db.UpdateUserGoal(id, goal);

            // update MacroGoals (everything) by userid
            _db.UpdateMacroGoalsByUserId(id, caloriesGoal, proteinGoal, carbsGoal, fatGoal);

            // update Calories - reset all totals to 0 
            _db.UpdateCaloriesByUserId(id, goal);

            string fullName = user.FirstName + " " + user.LastName;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            fullName = textInfo.ToTitleCase(fullName);

            PresentationModels.UserGoalsViewModel userGoals = new()
            {
                FullName = fullName,
                Goal = goalType,
                CaloriesGoal = caloriesGoal,
                ProteinGoal = proteinGoal,
                CarbsGoal = carbsGoal,
                FatGoal = fatGoal
            };

            List<DataAccessModels.FoodModel> totalFoodsLib = _db.GetAllMealsByUser(id);
            PresentationModels.TotalDailyMealsModel totals = new();

            foreach (DataAccessModels.FoodModel food in totalFoodsLib)
            {
                totals.TotalCalories += food.Calories;
                totals.TotalProtein += food.Protein;
                totals.TotalCarbs += food.Carbs;
                totals.TotalFat += food.Fat;
            } 

            PresentationModels.ViewModels.HomeViewModel homeViewModel = new()
            {
                UserGoals = userGoals,
                Totals = totals
            };
            
            return View("Index", homeViewModel); 
        }

        public IActionResult CreateFood()
        {
            return View("CreateFood"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFood(string name, string calories, string protein, string carbs, string fat)
        {
            double parsedCalories = double.Parse(calories, CultureInfo.InvariantCulture);
            double parsedProtein = double.Parse(protein, CultureInfo.InvariantCulture);
            double parsedCarbs = double.Parse(carbs, CultureInfo.InvariantCulture);
            double parsedFat = double.Parse(fat, CultureInfo.InvariantCulture);

            _db.InsertFood(name, parsedCalories, parsedProtein, parsedCarbs, parsedFat); 

            return RedirectToAction("Index"); 
        }
    }
}