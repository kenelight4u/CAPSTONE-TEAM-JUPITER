﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string TransactionId { get; set; }
        public DateTime PaymentDateTime { get; set; }
        [Required]
        public string PaymentStatus { get; set; } 
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual Order Order { get; set; }
        public string OrderId { get; set; }
    }
}