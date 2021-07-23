﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class EditCategoryRequest
    {
        [Required(ErrorMessage = "Id field is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }
    }
}