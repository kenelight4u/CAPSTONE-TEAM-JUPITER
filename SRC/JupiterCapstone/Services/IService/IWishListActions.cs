using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
   public interface IWishListActions
   {
        
        Task<bool> AddToWishListAsync(List<AddWishListItemDto> wishListItem, string UserId);

        Task<IEnumerable<ViewWishListItemDto>> GetWishListItemsAsync(string userId);

        Task RemoveWishListAsync(string userId, List<string> removeItemId);

   }
}
