using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        [Key]
        public string Id { get; set; }
        public virtual IEnumerable<CartItem> CartItems { get; set; }
        public double CartTotal { get; set; }
        public virtual Order Order  { get; set; }
        public string OrderId { get; set; }




    }
}
