using AutoMapper;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Profiles
{
    public class ShippingAddressProfile : Profile
    {
        public ShippingAddressProfile()
        {
            // Source => Target
            CreateMap<UsersAddress, ViewAddressDTO>();

            CreateMap<AddAddressDTO, UsersAddress>();

        }
    }
}
