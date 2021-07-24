using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public int StatusId { get; set; }
    }
}
