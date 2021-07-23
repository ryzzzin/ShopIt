﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.AdminModels
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "Comment field is required")]
        public string Text { get; set; }
        
    }
}
