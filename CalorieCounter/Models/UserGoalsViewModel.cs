namespace CalorieCounter.Models
{
    public class UserGoalsViewModel
    {
        public string FullName { get; set; }
        public string Goal { get; set; }
        public double TotalCalories { get; set; } = 0;
        public double TotalProtein { get; set; } = 0;
        public double TotalCarbs { get; set; } = 0;
        public double TotalFat { get; set; } = 0; 
        public double CaloriesGoal { get; set; }
        public double ProteinGoal { get; set; }
        public double CarbsGoal { get; set; }
        public double FatGoal { get; set; }

    }
}
