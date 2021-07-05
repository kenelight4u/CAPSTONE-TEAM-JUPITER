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

        private readonly IProduct _product;

        public WishListActions(ApplicationDbContext context, IProduct product)
        {
            _context = context;
            _product = product;
        }

        public async Task<bool> AddToWishListAsync(List<AddWishListItemDto> wishListItem)
        {
            if (wishListItem.Count==0)
            {
                return false;
            }

            foreach (var itemtoAdd in wishListItem)
            {

                var checkforItem = await _context.WishListItems.FirstOrDefaultAsync(e=>e.ProductId==itemtoAdd.ProductId && e.UserId==itemtoAdd.UserId);
                if (checkforItem==null)
                {
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
                else if (checkforItem.ProductId==itemtoAdd.ProductId)
                {
                    return false;
                }

            }
            return true;

        }

        public async Task<IEnumerable<ViewWishListItemDto>> GetWishListItemsAsync(string userId)
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
                        ItemId = wishList.ItemId,
                        ProductId = item.Id,
                        ProductUnitPrice = item.Price,
                        ProductDescription = item.Description,
                        SupplierName = item.SupplierName,
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        Status = item.Status,
                        ProductImage = item.ImageUrl,
                    });

                }              
               
            }

            return wishListDto;

        }

        public async Task RemoveWishListAsync(string userId, List<string> removeItemId)
        {
            var userWishList = await _context.WishListItems.Where(e => e.UserId == userId).ToListAsync();
            foreach (var itemToDelete in removeItemId)
            {
                var wishListToDelete =  userWishList.FirstOrDefault(e => e.ItemId ==itemToDelete);
                _context.WishListItems.Remove(wishListToDelete);
                await _context.SaveChangesAsync();

            }                             
            
        }

    }
}