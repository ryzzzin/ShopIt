using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class EditProductRequest
    {
        [Required(ErrorMessage = "Id field is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price field is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity field is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "CategoryId field is required")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Description field is required")]
        public string Description { get; set; }
    }
}
