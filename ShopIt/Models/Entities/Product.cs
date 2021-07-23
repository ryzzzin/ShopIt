using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string Title { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be more than 0")]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

        public Product()
        {
            Ratings = new List<Rating>();
        }
    }
}
