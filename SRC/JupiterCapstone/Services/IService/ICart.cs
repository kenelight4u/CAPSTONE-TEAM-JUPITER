using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    interface ICart
    {
        void AddItemtoCart(List<Cart> cart);
        void UpdateItemInCart(List<Cart> cart);
        void RemoveItemFromCart(List<Cart> cart );
        void CalculateItemInCart(List<Cart> cart);
        bool SaveChanges();
    }
}
