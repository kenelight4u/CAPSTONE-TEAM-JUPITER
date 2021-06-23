using JupiterCapstone.DTO;
using JupiterCapstone.Services.AuthorizationServices;
using JupiterCapstone.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IIdentityService
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LogIn login);

        Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request);
    }
}
