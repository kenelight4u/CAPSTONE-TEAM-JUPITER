﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.ViewModels.EditModels
{
    public class PaymentEM
    {
        public string Id { get; set; }
       
        public decimal Amount { get; set; }
       
        public string TransactionId { get; set; }
        public DateTime PaymentDateTime { get; set; }
      
        public string PaymentStatus { get; set; }
       
        public string UserId { get; set; }
       
        public string OrderId { get; set; }
    }
}
