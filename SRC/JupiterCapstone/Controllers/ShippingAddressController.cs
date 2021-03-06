using AutoMapper;
using JupiterCapstone.Data;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [Authorize]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingAddressService;

        private readonly IMapper _mapper;

        public ShippingAddressController(IShippingAddressService shippingAddressService, IMapper mapper)
        {
            _shippingAddressService = shippingAddressService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/AddShoppingAddress")]
        public ActionResult<ViewAddressDTO> AddShippingAddress(AddAddressDTO addressDTO)
        {
            var addressDTOmodel = _mapper.Map<UsersAddress>(addressDTO);
            _shippingAddressService.AddAddress(addressDTOmodel);
            _shippingAddressService.SaveChanges();
            
            var addressViewDto = _mapper.Map<ViewAddressDTO>(addressDTOmodel);

            return CreatedAtRoute(nameof(GetUserAdressById), new { addressViewDto.UserId }, addressViewDto); 
        }

        [HttpGet ("Get-UserAddress/{userId}", Name = "GetUserAdressById")]
        public ActionResult<IEnumerable<ViewAddressDTO>> GetUserAdressById(string userId)
        {
            var listOfAddresses = _shippingAddressService.GetAddressByUserId(userId);

            if (listOfAddresses == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ViewAddressDTO>>(listOfAddresses));
            
        }

        [HttpDelete]
        [Route("/DeleteUserAdress")]
        public IActionResult DeleteUserExpenses([FromQuery] string userId, [FromBody] List<string> addessIdToDelete)
        {
            _shippingAddressService.DeleteUserAddresse(userId, addessIdToDelete);

            return NoContent();
        }
    }
}
