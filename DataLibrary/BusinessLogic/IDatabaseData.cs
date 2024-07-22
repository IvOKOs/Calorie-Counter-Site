using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public interface IDatabaseData
    {
        int GetUserId(string email); 

        void InsertUser(string firstName,
                        string lastName,
                        string email,
                        string password,
                        string gender,
                        int age,
                        double height,
                        double weight,
                        int activity,
                        int goal);

        UserModel GetUserById(int id);

        void InsertMacroGoal(int userId, double calories, double proteinGoal, double carbsGoal, double fatGoal);

        int GetMacroGoalByUserId(int userId);

        void InsertInitialCalories(int userId, int macroGoalId);

        UserModel GetUserByEmailAndPass(string email, string pass);

        List<string> GetFoodNameBySearchTerm(string term);

        FoodModel GetFoodByName(string name);

        void InsertMeal(int userId, string foodName, double calories, double protein, double carbs,
                                double fat, double quantity, int mealId);

        List<MealModel> GetAllBreakfasts(int userId);

        List<MealModel> GetAllLunches(int userId);

        List<MealModel> GetAllSnacks(int userId);

        List<MealModel> GetAllDinners(int userId);

        List<FoodModel> GetAllMealsByUser(int id);

        List<int> GetUserIdCountByUserId(int id);

        void UpdateUserGoal(int id, int goal);

        void UpdateMacroGoalsByUserId(int id,
                                             double caloriesGoal,
                                             double proteinGoal,
                                             double carbsGoal,
                                             double fatGoal);

        void UpdateCaloriesByUserId(int id, int goalId);

        void UpdateTotalAmounts(int id,
                                       double totalCalories,
                                       double totalProtein,
                                       double totalCarbs,
                                       double totalFat);

        void InsertFood(string name, double calories, double protein, double carbs, double fat);

        List<QuoteModel> GetAllQuotes();

        List<ChallengeModel> GetAllChallenges();

        void InsertWeightHistory(WeightHistoryModel weightHistory);

        List<WeightHistoryModel> GetWeightHistoryByUserId(int userId);

        void UpdateUser(UserModel user);
    }
}
