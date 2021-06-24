using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        public virtual List<CartItem> Cartitems { get; set; }
        public double CartTotal { get; set; }
       

    }
}
