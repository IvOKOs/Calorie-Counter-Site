using System.ComponentModel.DataAnnotations;

namespace CalorieCounter.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
 
        public string LastName { get; set; }

        public string Email { get; set; }
  
        public string Password { get; set; }
 
        public string Gender { get; set; }
 
        public int Age { get; set; }
 
        public double Height { get; set; }

        public double Weight { get; set; }

        public int Activity { get; set; }
 
        public int Goal { get; set; }

    }
}
