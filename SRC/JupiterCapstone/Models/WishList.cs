using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class WishList
    {
        public WishList()
        {
            WishListItems = new HashSet<WishListItem>();
        }
        public string Id { get; set; }
        public virtual IEnumerable<WishListItem> WishListItems { get; set; } 
     
    }
}
