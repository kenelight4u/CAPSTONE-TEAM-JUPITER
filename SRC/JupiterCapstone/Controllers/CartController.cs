using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Services;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    
    [Authorize]
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
            var contextUser = ClaimsValidator.CheckClaimforUserId(HttpContext.User);
            if (contextUser==null)
            {
                return Unauthorized();
            }
            
            var checkquantity = _productAccess.CheckQuantityOfProducts(cartItem.ProductId);

            if (!checkquantity)
            {
                return BadRequest(new { message = "Product out of stock" });
            }
            var response = await _repository.AddToCartAsync(cartItem.ProductId, contextUser);

            if (!response) { return BadRequest(new { message = "Product couldn't be added to Shopping Cart" }); }
            return Ok(new {message= "Item Added to Cart" });
        }

        [HttpGet]
        [Route("get-cart-items")]
        public async Task<IActionResult> GetCartItems()
        {
            var contextUser = ClaimsValidator.CheckClaimforUserId(HttpContext.User);
            if (contextUser == null)
            {
                return Unauthorized();
            }

            var cartItems = await _repository.GetCartItemsAsync(contextUser);
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
        public async Task<IActionResult> GetCartTotal()
        {
            var contextUser = ClaimsValidator.CheckClaimforUserId(HttpContext.User);
            if (contextUser == null)
            {
                return Unauthorized();
            }
            var cartTotal =await _repository.GetCartTotalAsync(contextUser);
            return Ok(new { message = cartTotal }); 
        }

        [HttpDelete]
        [Route("empty-cart")]
        public async Task<IActionResult> EmptyCart()
        {
            var contextUser = ClaimsValidator.CheckClaimforUserId(HttpContext.User);
            if (contextUser == null)
            {
                return Unauthorized();
            }
            await _repository.EmptyCartAsync(contextUser);
            return NoContent();
        }

        [HttpPut]
        [Route("reduce-item-quantity")]
        public async Task<IActionResult> EditCartItemQuantity([FromBody]EditCartItemDto editCartItem)
        {
            var contextUser = ClaimsValidator.CheckClaimforUserId(HttpContext.User);
            if (contextUser == null)
            {
                return Unauthorized();
            }
            var response = await _repository.EditItemQuantityInCartAsync(editCartItem);

            if (!response)
            {
                return BadRequest(new { message="Quantity of Product cannot be lesser than 1" });
            }
            return NoContent();
        }

    }
}
