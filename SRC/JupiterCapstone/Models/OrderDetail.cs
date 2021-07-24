using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class OrderDetail 
    {
        [Key]
        public string Id { get; set; }
        public virtual User User  { get; set; }
        public string UserId { get; set; }
        public virtual UsersAddress UsersAddress { get; set; }
        public string UsersAddressId { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderId { get; set; } 
        public string ShippingStatus { get; set; }
        public string OrderStatus { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime Confirmed { get; set; }
        public DateTime Shippped { get; set; }
        public DateTime Delivered { get; set; }
        public DateTime Cancelled { get; set; } 
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
