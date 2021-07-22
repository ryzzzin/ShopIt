using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class ChangePasswordRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
