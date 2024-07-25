using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class SqlData : IDatabaseData
    {
        private readonly ISqlDataAccess _dataAccess;
        private const string connectionStringName = "SqlDb"; 

        public SqlData(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }


        public int GetUserId(string email)
        {
            return _dataAccess.LoadData<int, dynamic>("dbo.spUsers_GetId",
                                                      new { Email = email },
                                                      connectionStringName,
                                                      true).FirstOrDefault();  
        }

        public void InsertUser(string firstName,
                        string lastName,
                        string email,
                        string password,
                        string gender,
                        int age,
                        double height,
                        double weight,
                        int activity,
                        int goal) 
        {
            _dataAccess.SaveData<dynamic>("dbo.spUsers_Insert",
                                          new
                                          {
                                              FirstName = firstName,
                                              LastName = lastName,
                                              Email = email,
                                              Password = password,
                                              Gender = gender,
                                              Age = age,
                                              Height = height,
                                              Weight = weight,
                                              Activity = activity,
                                              Goal = goal 
                                          },
                                          connectionStringName,
                                          true); 
        } 

        public UserModel GetUserById(int id)
        {
            return _dataAccess.LoadData<UserModel, dynamic>("dbo.spUsers_GetUserById",
                                                            new { Id = id },
                                                            connectionStringName,
                                                            true).FirstOrDefault(); 
        }

        public void InsertMacroGoal(int userId, double calories, double proteinGoal, double carbsGoal, double fatGoal)
        {
            _dataAccess.SaveData<dynamic>("dbo.spMacroGoals_Insert",
                                          new
                                          {
                                              UserId = userId,
                                              CaloriesGoal = calories,
                                              ProteinGoal = proteinGoal,
                                              CarbsGoal = carbsGoal,
                                              FatGoal = fatGoal
                                          },
                                          connectionStringName,
                                          true); 
        }

        public int GetMacroGoalByUserId(int userId)
        {
            return _dataAccess.LoadData<int, dynamic>("dbo.spMacroGoals_GetByUserId",
                                                                 new { UserId = userId },
                                                                 connectionStringName,
                                                                 true).FirstOrDefault(); 
        }

        public void InsertInitialCalories(int userId, int macroGoalId)
        {
            _dataAccess.SaveData<dynamic>("dbo.spCalories_Insert",
                                          new { UserId = userId, MacroGoalId = macroGoalId },
                                          connectionStringName,
                                          true); 
        }

        public UserModel GetUserByEmailAndPass(string email, string pass)
        {
            return _dataAccess.LoadData<UserModel, dynamic>("dbo.spUsers_GetByEmailAndPass",
                                                            new { Email = email, Password = pass },
                                                            connectionStringName,
                                                            true).FirstOrDefault(); 
        }

        public List<string> GetFoodNameBySearchTerm(string term)
        {
            return _dataAccess.LoadData<string, dynamic>("dbo.spFoods_GetNameBySearchTerm",
                                                         new { Term = "%" + term + "%" },
                                                         connectionStringName,
                                                         true); 
        }

        public FoodModel GetFoodByName(string name)
        {
            return _dataAccess.LoadData<FoodModel, dynamic>("dbo.spFoods_GetFoodByName",
                                                            new { Name = name },
                                                            connectionStringName,
                                                            true).FirstOrDefault(); 
        }

        public void InsertMeal(int userId, string foodName, double calories, double protein, double carbs, 
                                double fat, double quantity, int mealId) 
        {
            _dataAccess.SaveData<dynamic>("dbo.spDailyMeals_InsertMeal",
                                                 new
                                                 {
                                                     UserId = userId,
                                                     FoodName = foodName,
                                                     Calories = calories,
                                                     Protein = protein,
                                                     Carbs = carbs,
                                                     Fat = fat,
                                                     Quantity = quantity,
                                                     MealId = mealId
                                                 },
                                                 connectionStringName,
                                                 true); 
        }

        public List<MealModel> GetAllBreakfasts(int userId)
        {
            return _dataAccess.LoadData<MealModel, dynamic>("dbo.spDailyMeals_LoadAllBreakfasts",
                                                                  new { UserId = userId },
                                                                  connectionStringName,
                                                                  true); 
        }

        public List<MealModel> GetAllLunches(int userId)
        {
            return _dataAccess.LoadData<MealModel, dynamic>("dbo.spDailyMeals_LoadAllLunches",
                                                                  new { UserId = userId },
                                                                  connectionStringName,
                                                                  true);
        }

        public List<MealModel> GetAllSnacks(int userId)
        {
            return _dataAccess.LoadData<MealModel, dynamic>("dbo.spDailyMeals_LoadAllSnacks",
                                                                  new { UserId = userId },
                                                                  connectionStringName,
                                                                  true);
        }

        public List<MealModel> GetAllDinners(int userId)
        {
            return _dataAccess.LoadData<MealModel, dynamic>("dbo.spDailyMeals_LoadAllDinners",
                                                                  new { UserId = userId },
                                                                  connectionStringName,
                                                                  true); 
        }

        public List<FoodModel> GetAllMealsByUser(int id)
        {
            return _dataAccess.LoadData<FoodModel, dynamic>("dbo.spDailyMeals_LoadAllMealsByUser",
                                                            new { UserId = id },
                                                            connectionStringName,
                                                            true); 
        }

        public List<int> GetUserIdCountByUserId(int id)
        {
            return _dataAccess.LoadData<int, dynamic>("dbo.spCalories_GetUserIdCountByUserId",
                                                      new { UserId = id },
                                                      connectionStringName,
                                                      true); 
        }

        public void UpdateUserGoal(int id, int goal)
        {
            _dataAccess.SaveData<dynamic>("dbo.spUsers_UpdateUserGoal",
                                          new { Id = id, Goal = goal },
                                          connectionStringName,
                                          true); 
        }

        public void UpdateMacroGoalsByUserId(int id,
                                             double caloriesGoal,
                                             double proteinGoal,
                                             double carbsGoal,
                                             double fatGoal)
        {
            _dataAccess.SaveData<dynamic>("dbo.spMacroGoals_UpdateMacroGoalByUserId",
                                          new
                                          {
                                              UserId = id,
                                              CaloriesGoal = caloriesGoal,
                                              ProteinGoal = proteinGoal,
                                              CarbsGoal = carbsGoal,
                                              FatGoal = fatGoal
                                          },
                                          connectionStringName,
                                          true); 
        }

        public void UpdateCaloriesByUserId(int id, int goalId)
        {
            _dataAccess.SaveData<dynamic>("dbo.spCalories_UpdateCaloriesAndGoalByUserId",
                                          new { UserId = id, MacroGoalId = goalId },
                                          connectionStringName,
                                          true); 
        }

        public void UpdateTotalAmounts(int id,
                                       double totalCalories,
                                       double totalProtein,
                                       double totalCarbs,
                                       double totalFat)
        {
            _dataAccess.SaveData<dynamic>("dbo.spCalories_UpdateTotals",
                                          new
                                          {
                                              UserId = id,
                                              TotalCalories = totalCalories,
                                              TotalProtein = totalProtein,
                                              TotalCarbs = totalCarbs,
                                              TotalFat = totalFat
                                          },
                                          connectionStringName,
                                          true); 
        }

        public void InsertFood(string name, double calories, double protein, double carbs, double fat)
        {
            _dataAccess.SaveData<dynamic>("dbo.spFoods_InsertFood",
                                          new
                                          {
                                              Name = name,
                                              Calories = calories,
                                              Protein = protein,
                                              Carbs = carbs,
                                              Fat = fat
                                          },
                                          connectionStringName,
                                          true); 
        }

        public List<QuoteModel> GetAllQuotes()
        {
            return _dataAccess.LoadData<QuoteModel, dynamic>("dbo.spQuotes_GetAllQuotes",
                                                 new { },
                                                 connectionStringName,
                                                 true); 
        }

        public List<ChallengeModel> GetAllChallenges()
        {
            return _dataAccess.LoadData<ChallengeModel, dynamic>("dbo.spChallenges_GetAllChallenges",
                                                            new { },
                                                            connectionStringName,
                                                            true); 
        }

        public void InsertWeightHistory(WeightHistoryModel weightHistory)
        {
            _dataAccess.SaveData<dynamic>("dbo.spWeightHistory_InsertWeightHistory",
                                            new 
                                            { 
                                                UserId = weightHistory.UserId,
                                                Weight = weightHistory.Weight,
                                                RecordedDate = weightHistory.RecordedDate,
                                            },
                                            connectionStringName,
                                            true);
        }

        public List<WeightHistoryModel> GetWeightHistoryByUserId(int userId)
        {
            return _dataAccess.LoadData<WeightHistoryModel, dynamic>("dbo.spWeightHistory_GetWeightHistoryByUserId",
                                                                new
                                                                {
                                                                    UserId = userId,
                                                                },
                                                                connectionStringName,
                                                                true);
        }

        public void UpdateUser(UserModel user)
        {
            _dataAccess.SaveData<dynamic>("dbo.spUsers_UpdateUser",
                                            new
                                            {
                                                Id = user.Id,
                                                FirstName = user.FirstName,
                                                LastName = user.LastName,
                                                Email = user.Email,
                                                Gender = user.Gender,
                                                Age = user.Age,
                                                Height = user.Height,
                                                Weight = user.Weight,
                                                Activity = user.Activity,
                                                Goal = user.Goal,
                                            },
                                            connectionStringName,
                                            true);
        }
    }
}
