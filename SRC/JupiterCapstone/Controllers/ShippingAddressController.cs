using AutoMapper;
using JupiterCapstone.Data;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingAddressService;

        private readonly IMapper _mapper;

        public ShippingAddressController(IShippingAddressService shippingAddressService, IMapper mapper, ApplicationDbContext context)
        {
            _shippingAddressService = shippingAddressService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddShoppingAddress")]
        public ActionResult<ViewAddressDTO> AddShippingAddress(AddAddressDTO addressDTO)
        {
            var addressDTOmodel = _mapper.Map<UsersAddress>(addressDTO);
            _shippingAddressService.AddAddress(addressDTOmodel);
            _shippingAddressService.SaveChanges();
            //_context.SaveChanges();

            var addressViewDto = _mapper.Map<ViewAddressDTO>(addressDTOmodel);

            return CreatedAtRoute("GetaddressById", new { addressViewDto.Id }, addressViewDto);
            
        }

        [HttpGet("{userId}", Name = "GetaddressById")]
        //[Route("GetAddressByUserId")]
        public ActionResult<IEnumerable<ViewAddressDTO>> GetaddressById(string userId)
        {
            var listOfAddress = _shippingAddressService.GetAddressByUserId(userId);

            if (listOfAddress == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ViewAddressDTO>>(listOfAddress));
        }

        [HttpDelete]
        [Route("DeleteUserAdress")]
        public IActionResult DeleteUserExpenses([FromQuery] string userId, [FromBody] List<string> addessIdToDelete)
        {
            _shippingAddressService.DeleteUserAddresse(userId, addessIdToDelete);

            return NoContent();
        }





    }
}
