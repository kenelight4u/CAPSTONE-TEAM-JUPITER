using JupiterCapstone.ViewModels.EditModels;
using JupiterCapstone.ViewModels.InputModels;
using JupiterCapstone.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IPayment
    {
        void PayNow();
        PaymentOM GetPaymentById(string paymentId);
        bool UpdatePayment(PaymentEM model);
        bool DeletePayment(string productId);
        bool AddPayment(PaymentIM model);
    }
}
