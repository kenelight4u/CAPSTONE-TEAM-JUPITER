﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Dtos.User
{
    public class AddPaymentDto
    {
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public string PaymentStatus { get; set; } 
    }
}
