using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IShoppingCartActions
    {
        Task<bool> AddToCartAsync(string productId, string userId);
        Task<IEnumerable<ViewCartItemDto>> GetCartItemsAsync(string userId);
        Task RemoveItemFromCartAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
        Task EmptyCartAsync(string userId);
        Task<bool> EditItemQuantityInCartAsync(EditCartItemDto editCartItem);
        
    }
}
