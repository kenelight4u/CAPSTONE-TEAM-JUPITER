using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.AuthorizationServices
{
    public class TokenConfiguration
    {
        public JwtSettings JwtSettings { get; set; }
    }


    public class JwtSettings
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }

    public class SmsConfiguration
    {
        public SmsSecretDetails SmsSecretDetails { get; set; }
    }

    public class SmsSecretDetails
    {
        public string AccountSID { get; set; }

        public string AuthToken { get; set; }
    }
}



