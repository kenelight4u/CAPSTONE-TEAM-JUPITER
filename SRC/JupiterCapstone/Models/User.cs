using JupiterCapstone.Services.AuthorizationServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }
            
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<RefreshToken> RefreshToken { get; set; }

        // will need them later

        //public virtual IEnumerable<Order> Orders { get; set; }

        //public virtual IEnumerable<Payment> Payments { get; set; }

        // public virtual IEnumerable<PaymentCard> PaymentCards { get; set; }

        // IEnumerable<CartItem> CartItems { get; set; }

       // public IEnumerable<WishListItem> WishListItems { get; set; }
    }
}
