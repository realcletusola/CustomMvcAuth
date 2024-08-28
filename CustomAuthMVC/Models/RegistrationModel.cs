using System.ComponentModel.DataAnnotations;

namespace CustomAuthMVC.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(30, ErrorMessage = "Max of 30 chracters is allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(30, ErrorMessage = "Max of 30 chracters is allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(60, MinimumLength =5, ErrorMessage = "Max of 60 and min of 5 chracters is allowed")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(20, ErrorMessage = "Max of 20 chracters is allowed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        // expression for password to be at least 8 char and must be a strong password
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Both passwords must be equal")]
        // expression for password to be at least 8 char and must be a strong password
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
