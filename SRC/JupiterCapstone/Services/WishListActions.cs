using JupiterCapstone.Data;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.EntityFrameworkCore;
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

       /* public void AddToWishList(string productId, string userId)
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
        }*/
        public async Task<bool> AddToWishList(List<AddWishListItemDto> wishListItem)
        {
            if (wishListItem.Count==0)
            {
                return false;
            }

            foreach (var itemtoAdd in wishListItem)
            {
                var checkforProduct = await _context.WishListItems.FirstOrDefaultAsync(e => e.ProductId == itemtoAdd.ProductId );

                //check if product is already there
                if (checkforProduct != null)
                {
                    return false;
                }

                WishListItem newwishItem = new WishListItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = itemtoAdd.ProductId,
                    UserId = itemtoAdd.UserId,
                    DateCreated = DateTime.Now
                };
                await _context.WishListItems.AddAsync(newwishItem);
                await _context.SaveChangesAsync();
               
            }
            return true;
     
        }

        public async Task<IEnumerable<ViewWishListItemDto>> GetWishListItems(string userId)
        {
            var userWishLists = await  _context.WishListItems.Where(e => e.UserId == userId).ToListAsync();
            if (userWishLists.Count==0)
            {
                return null;
            }
            List<ViewWishListItemDto> wishListDto = new List<ViewWishListItemDto>();
            foreach (var wishList in userWishLists)
            {
                var getproduct = await _context.Products.Where(e => e.Id == wishList.ProductId).ToListAsync();
                                
                foreach (var item in getproduct)
                {

                    wishListDto.Add(new ViewWishListItemDto
                    {
                        ProductId = item.Id,
                        Price = item.Price,
                        Description = item.Description,
                        SupplierName = item.SupplierName,
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        Status = item.Status,
                        ImageUrl = item.ImageUrl,
                    });

                }
                
               
            }

            return wishListDto;

        }

        public async Task RemoveWishList(string userId, List<string> removeProductId)
        {
            try
            {
                var userWishList = await _context.WishListItems.Where(e => e.UserId == userId).ToListAsync();
                //var myItem = await (from c in _context.WishListItems where c.UserId == userID && c.Product.Id == removeProductID select c).FirstOrDefaultAsync();
                foreach (var itemToDelete in removeProductId)
                {
                    var wishListTodelete = userWishList.FirstOrDefault(e => e.ProductId ==itemToDelete);
                    // Remove Item.
                    _context.WishListItems.Remove(wishListTodelete);
                    await _context.SaveChangesAsync();

                }                
                
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
            }

        }

    }
}

