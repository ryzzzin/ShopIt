﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.Entities
{
    public class AddedProducts : BaseEntity<Guid>
    {
        public Guid UserId {get; set;}
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
