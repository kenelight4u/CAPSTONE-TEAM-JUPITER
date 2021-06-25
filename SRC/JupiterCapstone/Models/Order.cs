using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class Order
    {
        public Order()
        {
            Carts = new HashSet<Cart>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required] 
        public string PaymentType { get; set; }
        public virtual IEnumerable<Cart> Carts { get; set; } 
    }
}
