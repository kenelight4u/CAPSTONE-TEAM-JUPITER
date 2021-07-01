using AutoMapper;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [ApiController]
    [Route("WishLists")]
    public class WishListsController : Controller
    {
        private readonly IWishListActions _wishList;

        private readonly IMapper _mapper;

        public WishListsController(IWishListActions wishList, IMapper mapper)
        {
            _wishList = wishList;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/GetWishListItemsById")]
        public ActionResult<ViewWishListItem> GetWishListItemsById(string userId)
        {
            var wishListItems = _wishList.GetWishListItems(userId);
            if (wishListItems == null)
                return NotFound();

            return Ok(_mapper.Map<ViewWishListItem>(wishListItems));
        }

        //discuss with Efe
        [HttpPost]
        [Route("/AddToWishList")]
        public ActionResult<ViewWishListItem> AddToAWishList(AddWishListItem wishListItem)
        {
            var wishlistModel = _mapper.Map<WishListItem>(wishListItem);
            _wishList.AddToWishList(wishlistModel.UserId, wishlistModel.ProductId);

            var viewWishListItem = _mapper.Map<ViewWishListItem>(wishlistModel);

            return CreatedAtRoute(nameof(GetWishListItemsById), new { Id = viewWishListItem.ItemId }, viewWishListItem);
        }


        //revisit, mistake
        [HttpDelete]
        [Route("/RemoveFromWishList")]
        public ActionResult RemoveItem(string userId, string productId)
        {
            var fromModel = _wishList.GetWishListItems(userId);
            if (fromModel == null)
                return NotFound();

            _wishList.RemoveItem(userId, productId);

            return NoContent();
        }

    }
}
