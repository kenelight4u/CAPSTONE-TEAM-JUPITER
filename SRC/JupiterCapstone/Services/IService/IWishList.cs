using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
   public interface IWishList
    {
        void AddToWishList(List<WishList> wishList);
        void RemoveWishList(List<WishList> wishList);
    }
}
