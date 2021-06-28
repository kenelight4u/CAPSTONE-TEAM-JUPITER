using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Dtos.Admin
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public string SubCategoryId { get; set; }
    }
}
