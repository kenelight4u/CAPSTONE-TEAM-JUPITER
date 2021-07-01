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
        private readonly IWishList _repository;
        public WishListsController(IWishList repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishList(List<AddWishListItemDto> addWishList)
        {
            var response = await _repository.AddToWishList(addWishList);
            if (!response)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetWishListItem([FromQuery] string userId)
        {
            var wishlist = await _repository.GetWishListItems(userId);
            if (wishlist==null)
            {
                return NotFound();
            }
            return Ok (wishlist);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteWishListItem([FromQuery] string userId, [FromBody] List<string> itemId)
        {
            await _repository.RemoveWishList(userId, itemId);
            return NoContent();

        }
    }
}
