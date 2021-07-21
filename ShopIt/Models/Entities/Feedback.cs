using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Models.Entities
{
    public class Feedback : BaseEntity<Guid>
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
