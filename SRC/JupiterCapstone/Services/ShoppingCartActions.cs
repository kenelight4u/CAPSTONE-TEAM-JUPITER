using JupiterCapstone.Data;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class ShoppingCartActions : IShoppingCartActions
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _productAccess; 

        public ShoppingCartActions(ApplicationDbContext context, IProduct productAccess)
        {
            _context = context;
            _productAccess = productAccess;

        }

        public async Task<bool> AddToCartAsync(string productId, string userId)
        {
            var checkProduct = await _context.Products.FirstOrDefaultAsync(e => e.Id == productId && e.Quantity !=0);
            if (checkProduct==null)
            {
                return false;
            }
            var checkUser = await _context.Users.FirstOrDefaultAsync(e => e.Id == userId);
            if (checkUser==null)
            {
                return false;
            }
            // Retrieve the product from the database.           
            var cartItem = await _context.ShoppingCartItems.SingleOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                
               await _context.ShoppingCartItems.AddAsync(cartItem);
                _productAccess.ReduceFromProductQuantity(productId);

            }
            else
            {
                var response = _productAccess.ReduceFromProductQuantity(productId);
                if (!response)
                {
                    return false;

                }
                // If the item does exist in the cart,                  
                // then add one to the quantity.
                cartItem.Quantity++;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ViewCartItemDto>> GetCartItemsAsync(string userId)
        {
            var userCartItems = await _context.ShoppingCartItems.Where(e => e.UserId == userId).ToListAsync();
            if (userCartItems.Count==0)
            {
                return null;
            }
            List<ViewCartItemDto> cartDto = new List<ViewCartItemDto>();
            foreach (var userCartItem in userCartItems)
            {
                var productCartItem = await _context.Products.FirstOrDefaultAsync(e => e.Id == userCartItem.ProductId);
                cartDto.Add(new ViewCartItemDto
                {
                    ItemId=userCartItem.ItemId,
                    ProductId=userCartItem.ProductId,
                    Quantity=userCartItem.Quantity,
                    ProductName= productCartItem.ProductName,
                    ProductImage= productCartItem.ImageUrl,
                    ProductDescription= productCartItem.Description,
                    Status= productCartItem.Status,
                    ProductUnitPrice= productCartItem.Price,
                    SupplierName= productCartItem.SupplierName,

                });
            }
            
            return cartDto;
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            // Multiply product price by quantity of that product to get        
            // the current price for each of those products in the cart.  
            // Sum all product price totals to get the cart total. 
            decimal total =await  _context.ShoppingCartItems.Where(x => x.UserId == userId).SumAsync(x => x.Quantity * x.Product.Price);
            return total;
        }

        public async Task RemoveItemFromCartAsync(string cartItemID)
        {   
            var userItem = await _context.ShoppingCartItems.Where(e => e.ItemId == cartItemID).FirstOrDefaultAsync();
            if (userItem != null)
            {
                _context.ShoppingCartItems.Remove(userItem);
                await _context.SaveChangesAsync();
            }  
            
        }

        public async Task EmptyCartAsync(string userId)
        {
            var cartItems = await _context.ShoppingCartItems.Where(c => c.UserId == userId).ToListAsync();

            foreach (var cartItem in cartItems)
            {
                _context.ShoppingCartItems.Remove(cartItem);
            }            
            await _context.SaveChangesAsync();
        }
        public async Task<bool> EditItemQuantityInCartAsync(EditCartItemDto editCartItem)
        {
            var cartItem = await _context.ShoppingCartItems.Where(e=>e.ItemId==editCartItem.ItemId).FirstOrDefaultAsync();
            //var productAccess = _productAccess.AddItemToProductQuantity(cartItem.ProductId);
            var availableQuantityAfterEdit = cartItem.Quantity - editCartItem.Quantity;

            cartItem.Quantity = availableQuantityAfterEdit;
            if (cartItem.Quantity<0 )
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
