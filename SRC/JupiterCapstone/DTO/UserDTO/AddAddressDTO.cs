using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.UserDTO
{
    public class AddAddressDTO
    {
        public string UserId { get; set; }

        public string Address { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }
    }
}
