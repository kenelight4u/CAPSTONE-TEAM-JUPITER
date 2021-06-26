using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO
{
    public class GoogleLoginRequest
    {
        public string IdToken { get; internal set; }

        public string Provider { get; set; }

        public string Key { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
