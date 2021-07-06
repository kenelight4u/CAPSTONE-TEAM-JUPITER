using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO
{
    public class UpdateUserDetails
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

    }
}
