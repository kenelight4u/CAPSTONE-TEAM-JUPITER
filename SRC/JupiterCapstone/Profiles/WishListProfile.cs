using AutoMapper;
using JupiterCapstone.DTO.UserDTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Profiles
{
    public class WishListProfile : Profile
    {
        public WishListProfile()
        {
            // Source => Target
            CreateMap<WishListItem, ViewWishListItem>();

            CreateMap<AddWishListItem, WishListItem>();
        }
    }
}
