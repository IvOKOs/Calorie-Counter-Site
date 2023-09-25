namespace CalorieCounter.Models
{
    public class FoodModel
    {
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public double Quantity { get; set; } = 100; 
    }
}
