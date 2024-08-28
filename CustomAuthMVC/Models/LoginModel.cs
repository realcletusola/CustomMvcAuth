using System.ComponentModel.DataAnnotations;

namespace CustomAuthMVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(20, ErrorMessage = "Max of 20 chracters is allowed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        // expression for password to be at least 8 char and must be a strong password
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
