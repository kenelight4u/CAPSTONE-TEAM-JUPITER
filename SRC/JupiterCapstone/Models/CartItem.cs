using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class CartItem 
    {
        [Key]
        public string Id { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public decimal Total { get; set; }
        public virtual Order Order { get; set; }
        public string OrderId { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }

    }
}
