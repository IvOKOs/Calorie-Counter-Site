namespace CalorieCounter.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserModel User { get; set; }
        public List<WeightHistoryModel> Weights { get; set; }
    }
}
