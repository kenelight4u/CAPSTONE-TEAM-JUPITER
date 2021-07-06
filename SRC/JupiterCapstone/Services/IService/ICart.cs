using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
   public  interface ICart
    {
        bool AddToCart(string productId, string userId);
        List<CartItem> GetCartItems(string userId);
        decimal GetCartTotal(string userId);
        bool RemoveItem(string removeCartID, string removeProductID);
        bool EmptyCart(string userId);
        //void AddItemtoCart(List<Cart> cart);
        //void UpdateItemInCart(List<Cart> cart);
        //void RemoveItemFromCart(List<Cart> cart );
        //void CalculateItemInCart(List<Cart> cart);
        //bool SaveChanges();
    }
}
