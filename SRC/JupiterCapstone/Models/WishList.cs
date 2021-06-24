using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class WishList
    {
        public string Id { get; set; }
        public virtual List<WishListItem> WishItems { get; set; } 
     
    }
}
