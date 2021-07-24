using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price field is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Stock field is required")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "CategoryId field is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Description field is required")]
        public string Description { get; set; }
    }
}
