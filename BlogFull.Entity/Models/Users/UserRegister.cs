using System.ComponentModel.DataAnnotations;

namespace BlogFull.Entity.Models.Users
{
    public class UserRegister : UserLogin
    {
        [MinLength(10, ErrorMessage = "Must be 10-30 characters")]
        [MaxLength(30, ErrorMessage = "Must be 10-30 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(30, ErrorMessage = "Can be at most 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
