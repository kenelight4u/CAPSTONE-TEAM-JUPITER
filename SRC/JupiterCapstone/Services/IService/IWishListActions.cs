using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
   public interface IWishListActions
    {
        void AddToWishList(string productId, string userId);

        List<WishListItem> GetWishListItems(string userId);

        void RemoveItem(string removeUserID, string removeProductID);
    }
}
