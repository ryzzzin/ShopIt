using System.ComponentModel.DataAnnotations;

namespace ShopIt.Models
{
    public class RegisterRequest
    {
        [Required]
        [Display(Name = "UserName")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password not same")]
        [Display(Name = "Confirm the password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}