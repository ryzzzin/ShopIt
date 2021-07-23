﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
        public Order()
        {
            OrderedProducts = new List<OrderedProducts>();
        }
    }
}
