using AutoMapper;
using JupiterCapstone.DTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // Source => Target
            CreateMap<User, ResetPassword>().ReverseMap();
            CreateMap<User, UpdateUserDetails>().ReverseMap();

        }
    }
}
