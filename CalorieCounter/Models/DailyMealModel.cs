namespace CalorieCounter.Models
{
    public class DailyMealModel
    {
        public List<FoodModel> Breakfasts { get; set; } = new List<FoodModel>();
        public List<FoodModel> Lunches { get; set; } = new List<FoodModel>();
        public List<FoodModel> Snacks { get; set; } = new List<FoodModel>();
        public List<FoodModel> Dinners { get; set; } = new List<FoodModel>(); 
    }
}
