using JupiterCapstone.DTO;
using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.GoogleServices.IGoogleService
{
    public interface IGoogleIdentity
    {
        Task<User> GetOrCreateExternalLoginUser(GoogleLoginRequest googleModel);
    }
}
