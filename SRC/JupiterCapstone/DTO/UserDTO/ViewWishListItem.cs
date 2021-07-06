using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.UserDTO
{
    public class ViewWishListItem
    {
        public string ItemId { get; set; }

        public virtual Product Product { get; set; }

        public string ProductId { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }

    }
}
