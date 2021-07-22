using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Email field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "UserName field is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
