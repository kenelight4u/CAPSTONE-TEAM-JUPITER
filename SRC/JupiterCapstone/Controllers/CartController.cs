using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [Route("api/v1/Cart")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICart _cart;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUrlHelper _urlHelper;

        public CartController(ICart cart, IHttpContextAccessor httpContext, IUrlHelper urlHelper)
        {
            _cart = cart;
            _httpContext = httpContext;
            _urlHelper = urlHelper;
        }
       

        [HttpDelete("Remove-CartItem/{CartItemId}/{ProductId}")]
        public async Task<IActionResult> RemoveItem(string CartItemId, string ProductId)
        {
            try
            {
                var response = await Task.Run(() => _cart.RemoveItem(CartItemId,ProductId));
                if (response != true)
                {
                    return NotFound("Item not found");
                }
                if (response == true)
                {
                    return Ok(true);
                }
                return BadRequest("Invalid request");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("EmptyCart/{UserId}")]
        public async Task<IActionResult> EmptyCart(string UserId)
        {
            try
            {
                var response = await Task.Run(() => _cart.EmptyCart(UserId));
                if (response != true)
                {
                    return NotFound(" Cart not empty");
                }
                if (response == true)
                {
                    return Ok(true);
                }
                return BadRequest("Invalid request");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("{userId}", Name = "GetByUserId")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var user = await Task.Run(() => _cart.GetCartItems(userId));
                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound("The response could not be found.");
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddToCart([FromBody] string productId, string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(userId))
                {
                    var result = await Task.Run(() => _cart.AddToCart(productId,userId));
                    if (result == true)
                    {
                        return Ok(result);
                    }
                    return BadRequest("An error occured");
                }
                string errors = string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
