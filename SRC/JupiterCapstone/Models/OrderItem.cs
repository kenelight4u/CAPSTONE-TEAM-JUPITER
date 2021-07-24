using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class OrderItem
    {
        [Key]
        public string Id { get; set; }
        public virtual Product Product { get; set; }
        public string ProductId { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public string OrderId { get; set; } 
    }
}
