using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.OutputModels
{
    public class PaymentOM
    {
        public string Id { get; set; }
        public decimal? Amount { get; set; }
        public string UserId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentStatus { get; set; }
        public string UserName { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
