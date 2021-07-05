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
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly IShoppingCartActions _repository;
        private readonly IProduct _productAccess;
        public CartController(IShoppingCartActions repository, IProduct product )
        {
            _repository = repository;
            _productAccess = product;
        }

        [HttpPost]
        [Route("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemDto cartItem)
        {
            var checkquantity = _productAccess.CheckQuantityOfProducts(cartItem.ProductId);
            if (!checkquantity)
            {
                return BadRequest(new { message = "Product out of stock" });
            }
            var response =await _repository.AddToCartAsync(cartItem.ProductId, cartItem.UserId);
            if (!response) { return BadRequest(new { message = "Product couldn't be added to Shopping Cart" }); }
            return Ok(new {message= "Item Added to Cart" });
        }

        [HttpGet]
        [Route("get-cart-items")]
        public async Task<IActionResult> GetCartItems([FromQuery]string userId)
        {
            var cartItems = await _repository.GetCartItemsAsync(userId);
            if (cartItems==null)
            {
                return NotFound();

            }
            return Ok(cartItems);
        }

        [HttpDelete]
        [Route("remove-cart-item")]
        public async Task<IActionResult> RemoveItemFromCart([FromQuery] string itemId)
        {
            await _repository.RemoveItemFromCartAsync(itemId);
            return NoContent();
        }

        [HttpGet]
        [Route("get-cart-total")]
        public async Task<IActionResult> GetCartTotal([FromQuery] string UserId)
        {
            var cartTotal =await _repository.GetCartTotalAsync(UserId);
            return Ok(new { message = cartTotal }); 
        }

        [HttpDelete]
        [Route("empty-cart")]
        public async Task<IActionResult> EmptyCart([FromQuery] string userId)
        {
            await _repository.EmptyCartAsync(userId);
            return NoContent();
        }

        [HttpPut]
        [Route("edit-item-quantity")]
        public async Task<IActionResult> EditCartItemQuantity([FromBody]EditCartItemDto editCartItem)
        {
           /* var checkquantity = _productAccess.CheckQuantityOfProducts(editCartItem.ProductId);
            if (!checkquantity)
            {
                return BadRequest(new { message = "Product out of stock" });
            }*/
            var response=await _repository.EditItemQuantityInCartAsync(editCartItem);
            if (!response)
            {
                return BadRequest(new { message="Quantity of Product cannot be lesser than 1" });
            }
            return NoContent();
        }

    }
}
