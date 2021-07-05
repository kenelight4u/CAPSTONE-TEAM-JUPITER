using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.InputModels
{
    public class OrderIM
    {
        public string Id { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public string Status { get; set; }
       
        public string PaymentType { get; set; }
      //  public virtual IEnumerable<Cart> Carts { get; set; }
    }
}
