using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class UsersAddress
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(256)]
        public string Address { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public virtual User User { get; set; }
       
    }
}
