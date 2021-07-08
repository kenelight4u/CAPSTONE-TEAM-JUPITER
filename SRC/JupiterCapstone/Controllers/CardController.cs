using JupiterCapstone.DTO.EditModels;
using JupiterCapstone.DTO.InputModels;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [ApiController]

    public class CardController : ControllerBase
    {

        private readonly IPayment _payment;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUrlHelper _urlHelper;

        public CardController(IPayment payment, IHttpContextAccessor httpContext, IUrlHelper urlHelper)
        {
            _payment = payment;
            _httpContext = httpContext;
            _urlHelper = urlHelper;
        }

        [HttpPut]
        [Route("/UpdateCardDetails")]
        public async Task<IActionResult> UpdateCardDetails([FromBody] CardEM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Task.Run(() => _payment.UpdateCardDetails(model));
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

        [HttpDelete("Remove-CardDetails/{cardid}")]
        public async Task<IActionResult> DeleteCardDetail(string cardid)
        {
            try
            {
                var response = await Task.Run(() => _payment.DeleteCardDetail(cardid));
                if (response != true)
                {
                    return NotFound("Card detail not found");
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



        [HttpPost]
        [Route("/AddCardDetail")]
        public async Task<IActionResult> AddCardDetail([FromBody] CardIM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Task.Run(() => _payment.AddCardDetail(model));
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
