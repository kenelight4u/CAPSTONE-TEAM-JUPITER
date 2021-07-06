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
            CardDetails = new HashSet<CardDetail>();
            CartItems = new HashSet<CartItem>();
            UsersAddress = new HashSet<UsersAddress>();
            WishListItems = new HashSet<WishListItem>();
            Payments = new HashSet<Payment>();
            Orders = new HashSet<Order>();
        }

        public override string PasswordHash { get; set; }

        public string ResetPasswordToken { get; set; }

        public string ProfilePicture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<RefreshToken> RefreshToken { get; set; }

        public IEnumerable<CardDetail> CardDetails { get; set; }

        public IEnumerable<UsersAddress> UsersAddress { get; set; } 

        public virtual IEnumerable<Order> Orders { get; set; }

        public virtual IEnumerable<Payment> Payments { get; set; }

        public virtual IEnumerable<CartItem> CartItems { get; set; }

        public IEnumerable<WishListItem> WishListItems { get; set; }
    }
}
