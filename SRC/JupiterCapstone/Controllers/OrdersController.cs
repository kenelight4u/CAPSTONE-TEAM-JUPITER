using JupiterCapstone.Services.IService;
using JupiterCapstone.ViewModels.EditModels;
using JupiterCapstone.ViewModels.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{

    [Route("api/v1/Order")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrder _order;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUrlHelper _urlHelper;

        public OrdersController(IOrder order, IHttpContextAccessor httpContext, IUrlHelper urlHelper)
        {
            _order = order;
            _httpContext = httpContext;
            _urlHelper = urlHelper;
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderEM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Task.Run(() => _order.UpdateOrder(model));
                    if (result == true)
                    {
                        return Ok(true);
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

        [HttpDelete("Remove-Order/{orderId}")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            try
            {
                var response = await Task.Run(() => _order.DeleteOrder(orderId));
                if (response != true)
                {
                    return NotFound("Order not found");
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
        [Route("{orderId}", Name = "GetOrderById")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            try
            {
                var order = await Task.Run(() => _order.GetOrderById(orderId));
                return Ok(order);
            }
            catch (Exception)
            {
                return NotFound("The Order requested for could not be found.");
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderIM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Task.Run(() => _order.AddOrder(model));
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
