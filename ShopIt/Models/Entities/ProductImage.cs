using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.Entities
{
    public class ProductImage : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Path { get; set; }
    }
}
