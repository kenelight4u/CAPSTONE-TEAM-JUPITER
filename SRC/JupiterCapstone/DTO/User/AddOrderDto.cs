using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Dtos.User
{
    public class AddOrderDto
    {
        public decimal TotalPrice { get; set; }
        public string  Status  { get; set; }


    }
}
