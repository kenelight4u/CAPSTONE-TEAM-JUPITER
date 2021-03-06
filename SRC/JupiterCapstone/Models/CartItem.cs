using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class CartItem 
    {
        [Key]
        public string ItemId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        ////not yet used
        //[Required]
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal Total { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
        // public virtual Cart Cart  { get; set; }
        //public string CartId { get; set; } 
    }
}
