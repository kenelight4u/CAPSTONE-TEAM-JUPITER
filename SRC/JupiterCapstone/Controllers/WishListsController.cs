using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [ApiController]
    [Route("Wishlist")]
    public class WishListsController : Controller
    { 
        private readonly IWishListActions _repository;
        private readonly IProduct _product; 
        public WishListsController(IWishListActions repository, IProduct product)
        {
            _repository = repository;
            _product = product;
        }

        [HttpPost]
        [Route("add-items-towishlist")]
        public async Task<IActionResult> AddToWishList([FromBody]List<AddWishListItemDto> addWishList)
        {
            foreach (var item in addWishList)
            {
                var checkquantity = _product.CheckQuantityOfProducts(item.ProductId);
                if (!checkquantity)
                {
                    return BadRequest(new { message = "Product out of stock" });
                }
            }
            var response = await _repository.AddToWishListAsync(addWishList);
            if (!response)
            {
                return BadRequest(new { message="Item already exists in your wishList"});
            }
            return Ok(new { message="Item Added to wishLists"});
        }

        [HttpGet]
        [Route("get-all-wishitems")]
        public async Task<IActionResult> GetWishListItem([FromQuery] string userId)
        {
            var wishlist = await _repository.GetWishListItemsAsync(userId);
            if (wishlist==null)
            {
                return NotFound();
            }
            return Ok (wishlist);
        }

        [HttpDelete]
        [Route("remove-wishlist-items")]
        public async Task<IActionResult> DeleteWishListItem([FromQuery] string userId, [FromBody] List<string> itemId)
        {
            await _repository.RemoveWishListAsync(userId, itemId);
            return NoContent();

        }

    }
}
