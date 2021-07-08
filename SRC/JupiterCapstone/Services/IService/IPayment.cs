using JupiterCapstone.DTO.EditModels;
using JupiterCapstone.DTO.InputModels;
using JupiterCapstone.DTO.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IPayment
    {
        PaymentOM GetPaymentById(string paymentId);
        bool UpdatePayment(PaymentEM model);
        bool DeletePayment(string productId);
        bool AddPayment(PaymentIM model);
        IEnumerable<PaymentOM> GetUserPayments(string userId);
        bool AddCardDetail(CardIM model);

        bool UpdateCardDetails(CardEM model);
        bool DeleteCardDetail(string cardId);
    }
}
  
