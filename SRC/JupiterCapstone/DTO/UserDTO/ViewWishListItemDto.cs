using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.UserDTO
{
    public class ViewWishListItemDto
    {
        public string ItemId { get; set; }
        public string ProductId { get; set; }
        public double Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public string ProductImage { get; set; }
        public string Status { get; set; }
        public string SupplierName { get; set; }

        //public int TotalQuantityAvailable { get; set; }
        //public int QuantityOfProductInCart { get; set; }
    }
}
