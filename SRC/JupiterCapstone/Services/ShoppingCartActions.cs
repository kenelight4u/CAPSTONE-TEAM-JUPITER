using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class ShoppingCartActions
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartActions(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddToCart(string productId, string userId)
        {
            // Retrieve the product from the database.           
            var cartItem = _context.ShoppingCartItems.SingleOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = productId,
                    UserId = userId,
                    Product = _context.Products.SingleOrDefault(p => p.Id == productId && p.Quantity != 0),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems(string userId)
        {
            
            return _context.ShoppingCartItems.Where( c => c.UserId == userId).ToList();
        }

        public decimal GetCartTotal(string userId)
        {
            // Multiply product price by quantity of that product to get        
            // the current price for each of those products in the cart.  
            // Sum all product price totals to get the cart total. 
            decimal total = _context.ShoppingCartItems.Where(x => x.UserId == userId).Sum(x => x.Quantity * x.Product.Price);
            
            return total;
        }

        public void RemoveItem(string removeCartID, string removeProductID)
        {
            
            try
            {
                var myItem = (from c in _context.ShoppingCartItems where c.UserId == removeCartID && c.Product.Id == removeProductID select c).FirstOrDefault();
                if (myItem != null)
                {
                    // Remove Item.
                    _context.ShoppingCartItems.Remove(myItem);
                    _context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
            }
            
        }

        public void EmptyCart(string userId)
        {

            var cartItems = _context.ShoppingCartItems.Where(
                c => c.UserId == userId);
            foreach (var cartItem in cartItems)
            {
                _context.ShoppingCartItems.Remove(cartItem);
            }
           
            // Save changes.             
            _context.SaveChanges();
        }
    }
}
