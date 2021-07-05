using JupiterCapstone.Services.IService;
using JupiterCapstone.DTO.EditModels;
using JupiterCapstone.DTO.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    //[Route("api/v1/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _payment;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUrlHelper _urlHelper;

        public PaymentController(IPayment payment, IHttpContextAccessor httpContext, IUrlHelper urlHelper)
        {
            _payment = payment;
            _httpContext = httpContext;
            _urlHelper = urlHelper;
        }


        [HttpPut]
        [Route("/UpdatePayment")]
        public async Task<IActionResult> UpdatePayment([FromBody] PaymentEM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Task.Run(() => _payment.UpdatePayment(model));
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

        [HttpDelete("Remove-Payment/{paymentId}")]
        public async Task<IActionResult> DeletePayment(string paymentId)
        {
            try
            {
                var response = await Task.Run(() => _payment.DeletePayment(paymentId));
                if (response != true)
                {
                    return NotFound("Payment not found");
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
        [Route("Get-Payment/{paymentId}", Name = "GetPaymentById")]
        public async Task<IActionResult> GetPaymentById(string paymentId)
        {
            try
            {
                var payment = await Task.Run(() => _payment.GetPaymentById(paymentId));
                return Ok(payment);
            }
            catch (Exception)
            {
                return NotFound("The Payment requested for could not be found.");
            }
        }

        [HttpPost]
        [Route("/CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentIM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Task.Run(() => _payment.AddPayment(model));
                    if (result==true)
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
