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
        Task<bool> AddToWishListAsync(List<AddWishListItemDto> wishListItem);
        Task RemoveWishListAsync(string removeUserID, List<string> removeProductID);
        Task <IEnumerable<ViewWishListItemDto>> GetWishListItemsAsync(string userId);
    }
}
