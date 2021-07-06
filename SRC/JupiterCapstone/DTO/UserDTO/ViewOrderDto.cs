using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.UserDTO
{
    public class ViewOrderDto
    {
        public string OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
