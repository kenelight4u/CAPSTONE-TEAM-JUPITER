using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class UsersAddress
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
