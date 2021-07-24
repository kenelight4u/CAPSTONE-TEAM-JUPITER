using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class ClaimsValidator
    {  
        public static string CheckClaimforUserId(ClaimsPrincipal contextUser)
        {
            var checkUserClaim = contextUser.HasClaim(e=>e.Type=="UserId");
            if (!checkUserClaim)
            {
                return null;

            }
            else
            {
                var userIdClaim = contextUser.FindFirst(e => e.Type == "UserId");
                return userIdClaim.Value;
            }

        }

    }
}
