using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class WishListActions : IWishListActions
    {
        private readonly ApplicationDbContext _context;

        public WishListActions(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddToWishList(string productId, string userId)
        {
            // Retrieve the product from the database.
            var wishItem = _context.WishListItems.SingleOrDefault(w => w.UserId == userId && w.ProductId == productId);
            
            if (wishItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                wishItem = new WishListItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = productId,
                    UserId = userId,
                    Product = _context.Products.SingleOrDefault(p => p.Id == productId && p.Quantity != 0),
                    DateCreated = DateTime.Now
                };

                _context.WishListItems.Add(wishItem);
            }
            
            _context.SaveChanges();
        }

        public List<WishListItem> GetWishListItems(string userId)
        {
            return _context.WishListItems.Where(w => w.UserId == userId).ToList();
        }
        
        public void RemoveItem(string removeUserID, string removeProductID)
        {

            try
            {
                var myItem = (from c in _context.WishListItems where c.UserId == removeUserID && c.Product.Id == removeProductID select c).FirstOrDefault();

                if (myItem != null)
                {
                    // Remove Item.
                    _context.WishListItems.Remove(myItem);
                    _context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
            }

        }

    }
}

