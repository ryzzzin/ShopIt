using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string ProductInfo { get; set; }
        public string UserInfo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
