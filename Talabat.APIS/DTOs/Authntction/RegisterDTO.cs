using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs.Authntction
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Display name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string DisplayName { get; set; }

        //[Required]
        //[Phone(ErrorMessage = "Invalid phone number format.")]
        //public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
