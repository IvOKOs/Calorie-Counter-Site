using PresentationModels = CalorieCounter.Models;
using DataAccessModels = DataLibrary.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;
using CalorieCounter.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace CalorieCounter.Controllers
{
    public class FoodController : Controller
    {
        private readonly IDatabaseData _db;

        public FoodController(IDatabaseData db)
        {
            _db = db; 
        }
        // GET: FoodController
        public ActionResult Index()
        {
            string foodJson = TempData["FoodModel"] as string;
            string dailyMealJson = TempData["DailyMealModel"] as string;
            //string dailyMealsJsonCookie = Request.Cookies["DailyMealsCookie"];

            int userId = 0; 
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim != null)
            {
                userId = int.Parse(userIdClaim.Value);
            }

            FoodModel food = new FoodModel();  
            if (foodJson != null)
            {
               food  = JsonConvert.DeserializeObject<FoodModel>(foodJson); 
            }

            DailyMealModel dailyMeals = new DailyMealModel(); 
            if(dailyMealJson != null)
            {
                dailyMeals = JsonConvert.DeserializeObject<DailyMealModel>(dailyMealJson); 
            }
            else
            {
                if(userId != 0)
                {
                    List<DataAccessModels.MealModel> BreakfastMeals = _db.GetAllBreakfasts(userId);
                    List<FoodModel> Breakfasts = new List<FoodModel>();
                    foreach (var breakfastMeal in BreakfastMeals)
                    {
                        FoodModel breakfast = new FoodModel()
                        {
                            Name = breakfastMeal.FoodName,
                            Calories = breakfastMeal.Calories,
                            Protein = breakfastMeal.Protein,
                            Carbs = breakfastMeal.Carbs,
                            Fat = breakfastMeal.Fat,
                            Quantity = breakfastMeal.Quantity
                        };

                        Breakfasts.Add(breakfast);
                    }
 
                    List<DataAccessModels.MealModel> LunchMeals = _db.GetAllLunches(userId);
                    List<FoodModel> Lunches = new List<FoodModel>();
                    foreach (var lunchMeal in LunchMeals)
                    {
                        FoodModel lunch = new FoodModel()
                        {
                            Name = lunchMeal.FoodName,
                            Calories = lunchMeal.Calories,
                            Protein = lunchMeal.Protein,
                            Carbs = lunchMeal.Carbs,
                            Fat = lunchMeal.Fat,
                            Quantity = lunchMeal.Quantity
                        };

                        Lunches.Add(lunch);
                    }

                    List<DataAccessModels.MealModel> SnackMeals = _db.GetAllSnacks(userId);
                    List<FoodModel> Snacks = new List<FoodModel>();
                    foreach (var snackMeal in SnackMeals)
                    {
                        FoodModel snack = new FoodModel()
                        {
                            Name = snackMeal.FoodName,
                            Calories = snackMeal.Calories,
                            Protein = snackMeal.Protein,
                            Carbs = snackMeal.Carbs,
                            Fat = snackMeal.Fat,
                            Quantity = snackMeal.Quantity
                        };

                        Snacks.Add(snack);
                    }

                    List<DataAccessModels.MealModel> DinnerMeals = _db.GetAllDinners(userId);
                    List<FoodModel> Dinners = new List<FoodModel>();
                    foreach (var dinnerMeal in DinnerMeals)
                    {
                        FoodModel dinner = new FoodModel()
                        {
                            Name = dinnerMeal.FoodName,
                            Calories = dinnerMeal.Calories,
                            Protein = dinnerMeal.Protein,
                            Carbs = dinnerMeal.Carbs,
                            Fat = dinnerMeal.Fat,
                            Quantity = dinnerMeal.Quantity
                        };

                        Dinners.Add(dinner);
                    }

                    dailyMeals = new DailyMealModel()
                    {
                        Breakfasts = Breakfasts,
                        Lunches = Lunches,
                        Snacks = Snacks,
                        Dinners = Dinners
                    };
                }
            }

            TotalDailyMealsModel totals = new(); 

            if(dailyMeals != null)
            {
                foreach(var dailyMeal in dailyMeals.Breakfasts)
                {
                    totals.TotalCalories += dailyMeal.Calories;
                    totals.TotalProtein += dailyMeal.Protein;
                    totals.TotalCarbs += dailyMeal.Carbs;
                    totals.TotalFat += dailyMeal.Fat;
                }

                foreach (var dailyMeal in dailyMeals.Lunches)
                {
                    totals.TotalCalories += dailyMeal.Calories;
                    totals.TotalProtein += dailyMeal.Protein;
                    totals.TotalCarbs += dailyMeal.Carbs;
                    totals.TotalFat += dailyMeal.Fat;
                }

                foreach (var dailyMeal in dailyMeals.Snacks)
                {
                    totals.TotalCalories += dailyMeal.Calories;
                    totals.TotalProtein += dailyMeal.Protein;
                    totals.TotalCarbs += dailyMeal.Carbs;
                    totals.TotalFat += dailyMeal.Fat;
                }

                foreach (var dailyMeal in dailyMeals.Dinners)
                {
                    totals.TotalCalories += dailyMeal.Calories;
                    totals.TotalProtein += dailyMeal.Protein;
                    totals.TotalCarbs += dailyMeal.Carbs;
                    totals.TotalFat += dailyMeal.Fat;
                }
            }
            
            FoodDailyMealsViewModel foodViewModel = new()
            {
                Food = food, 
                DailyMeals = dailyMeals,
                Totals = totals 
            }; 

            return View(foodViewModel); 
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PopulateFood(string foodName)
        {
            // get the Food by Name 
            DataAccessModels.FoodModel foodModel = _db.GetFoodByName(foodName);
            string foodJson = null; 

            if(foodModel != null)
            {
                FoodModel food = new FoodModel
                {
                    Name = foodModel.Name,
                    Calories = foodModel.Calories,
                    Protein = foodModel.Protein,
                    Carbs = foodModel.Carbs,
                    Fat = foodModel.Fat
                };
  
                foodJson = JsonConvert.SerializeObject(food);
            }
            
            TempData["FoodModel"] = foodJson; 
            return RedirectToAction("Index");  
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PopulateDailyMeals(string foodName, string quantity, string meal, string calories,
                                               string protein, string carbs, string fat)
        {
            if(foodName == null)
            {
                return RedirectToAction("Index"); 
            }

            int mealType = 0;
            int userId = 0;
            string dailyMealJson; 
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if(userIdClaim != null)
            {
               userId  = int.Parse(userIdClaim.Value); 
            }
             
            switch (meal)
            {
                case "Breakfast": mealType = 1; break;
                case "Lunch": mealType = 2; break;
                case "Snack": mealType = 3; break;
                case "Dinner": mealType = 4; break; 
            }

            quantity = quantity.Replace(",", ".");
            calories = calories.Replace(",", ".");
            protein = protein.Replace(",", ".");
            carbs = carbs.Replace(",", ".");
            fat = fat.Replace(",", "."); 

            // parse the values 
            double parsedQuantity = double.Parse(quantity, CultureInfo.InvariantCulture);
            double parsedCalories = double.Parse(calories, CultureInfo.InvariantCulture);
            double parsedProtein = double.Parse(protein, CultureInfo.InvariantCulture);
            double parsedCarbs = double.Parse(carbs, CultureInfo.InvariantCulture);
            double parsedFat = double.Parse(fat, CultureInfo.InvariantCulture);

            // insert data for user into db - DailyMeals 
            _db.InsertMeal(userId, foodName, parsedCalories, parsedProtein, parsedCarbs, parsedFat, parsedQuantity, mealType);

            // retrieve all data for user with mealType = 1 and insert into Breakfasts 
            List<DataAccessModels.MealModel> BreakfastMeals = _db.GetAllBreakfasts(userId);
            List<FoodModel> Breakfasts = new List<FoodModel>(); 
            foreach (var breakfastMeal in BreakfastMeals)
            {
                FoodModel breakfast = new FoodModel()
                {
                    Name = breakfastMeal.FoodName,
                    Calories = breakfastMeal.Calories,
                    Protein = breakfastMeal.Protein,
                    Carbs = breakfastMeal.Carbs,
                    Fat = breakfastMeal.Fat,
                    Quantity = breakfastMeal.Quantity
                }; 

                Breakfasts.Add(breakfast); 
            }

            // do the same for the next lists 
            List<DataAccessModels.MealModel> LunchMeals = _db.GetAllLunches(userId);
            List<FoodModel> Lunches = new List<FoodModel>(); 
            foreach(var lunchMeal in LunchMeals)
            {
                FoodModel lunch = new FoodModel()
                {
                    Name = lunchMeal.FoodName,
                    Calories = lunchMeal.Calories,
                    Protein = lunchMeal.Protein,
                    Carbs = lunchMeal.Carbs,
                    Fat = lunchMeal.Fat,
                    Quantity = lunchMeal.Quantity
                };

                Lunches.Add(lunch); 
            }

            List<DataAccessModels.MealModel> SnackMeals = _db.GetAllSnacks(userId);
            List<FoodModel> Snacks = new List<FoodModel>(); 
            foreach(var snackMeal in SnackMeals)
            {
                FoodModel snack = new FoodModel()
                {
                    Name = snackMeal.FoodName,
                    Calories = snackMeal.Calories,
                    Protein = snackMeal.Protein,
                    Carbs = snackMeal.Carbs,
                    Fat = snackMeal.Fat,
                    Quantity = snackMeal.Quantity
                }; 

                Snacks.Add(snack); 
            }

            List<DataAccessModels.MealModel> DinnerMeals = _db.GetAllDinners(userId);
            List<FoodModel> Dinners = new List<FoodModel>(); 
            foreach(var dinnerMeal in DinnerMeals)
            {
                FoodModel dinner = new FoodModel()
                {
                    Name = dinnerMeal.FoodName,
                    Calories = dinnerMeal.Calories,
                    Protein = dinnerMeal.Protein,
                    Carbs = dinnerMeal.Carbs,
                    Fat = dinnerMeal.Fat,
                    Quantity = dinnerMeal.Quantity
                }; 

                Dinners.Add(dinner); 
            }

            DailyMealModel dailyMeal = new DailyMealModel()
            {
                Breakfasts = Breakfasts,
                Lunches = Lunches, 
                Snacks = Snacks, 
                Dinners = Dinners
            };

            dailyMealJson = JsonConvert.SerializeObject(dailyMeal);

            Response.Cookies.Append("DailyMealsCookie", dailyMealJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            }); 

            TempData["DailyMealModel"] = dailyMealJson; 
            return RedirectToAction("Index"); 
        }



        [System.Web.Mvc.Route("Food/AutoComplete")]
        public JsonResult AutoComplete(string term)
        {
            List<string> foodNames = _db.GetFoodNameBySearchTerm(term);

            return new JsonResult(foodNames, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });  
        }

        [HttpPost]
        public IActionResult CalculateNutrition([FromBody] QuantityModel model)
        {
            NutritionModel nutrition; 
            // Retrieve the quantity value from the model
            double quantity = model.Quantity;
            string name = model.Name;

            // Perform the necessary calculations to determine the nutrition values
            DataAccessModels.FoodModel food = _db.GetFoodByName(name);

            if(food == null)
            {
                nutrition = new NutritionModel
                {
                    Calories = 0,
                    Protein = 0,
                    Carbs = 0,
                    Fat = 0
                }; 
            }
            else
            {
                double caloriesPerGram = food.Calories / 100;
                double proteinPerGram = food.Protein / 100;
                double carbsPerGram = food.Carbs / 100;
                double fatPerGram = food.Fat / 100;

                double calculatedCalories = caloriesPerGram * quantity;
                double calculatedProtein = proteinPerGram * quantity;
                double calculatedCarbs = carbsPerGram * quantity;
                double calculatedFat = fatPerGram * quantity;

                // Create an object to hold the calculated nutrition values
                nutrition = new NutritionModel
                {
                    Calories = calculatedCalories,
                    Protein = calculatedProtein,
                    Carbs = calculatedCarbs,
                    Fat = calculatedFat
                };
            }
            
            // Return the nutrition values as JSON
            return Json(nutrition);
        }

    }
}
