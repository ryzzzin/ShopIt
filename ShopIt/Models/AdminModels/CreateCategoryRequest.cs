using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }
        
    }
}
