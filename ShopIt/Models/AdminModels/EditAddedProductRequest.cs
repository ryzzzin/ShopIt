using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class EditAddedProductRequest
    {
        [Required(ErrorMessage = "Id field is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "ProductId field is required")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Quantity field is required")]
        public int Quantity { get; set; }
    }
}
