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
        Task<bool> AddToWishList(List<AddWishListItemDto> wishListItem);

        Task RemoveWishList(string removeUserID, List<string> removeProductID);

        Task <IEnumerable<ViewWishListItemDto>> GetWishListItems(string userId);
   }
}
