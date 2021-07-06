using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using JupiterCapstone.DTO.EditModels;
using JupiterCapstone.DTO.InputModels;
using JupiterCapstone.DTO.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace JupiterCapstone.Services
{
  
    
    public class PaymentAccess : IPayment
    {
        private readonly ApplicationDbContext _context;
        public PaymentAccess(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool AddPayment(PaymentIM model)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var _model = new Payment
                {
                    Id = model.Id,
                    PaymentDateTime = model.PaymentDateTime,
                    TransactionId = model.TransactionId,
                    PaymentStatus = model.PaymentStatus,
                    UserId = model.UserId,
                    //    ProductId = model.ProductId,
                    Amount = model.Amount,
                };
                _context.Payments.Add(_model);

                int bit = _context.SaveChanges();
                ts.Complete();
                if (bit > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeletePayment(string paymentId)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var payment = _context.Payments.Where(s => s.Id == paymentId).FirstOrDefault();
                if (payment == null)
                {
                    return false;
                }
                _context.Payments.Remove(payment);

                _context.SaveChanges();
                ts.Complete();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PaymentOM GetPaymentById(string paymentId)
        {
            var payment = _context.Payments.Find(paymentId);
            if (payment != null)
            {
                var resp = new PaymentOM
                {
                    Id = payment.Id,
                    PaymentDate = payment.PaymentDateTime,
                    TransactionId = payment.TransactionId,
                    PaymentStatus = payment.PaymentStatus,
                
                    UserId = payment.UserId,
                    UserName = payment.User.FirstName +" " + payment.User.LastName,
                  
                    Amount = payment.Amount,
                };
                return resp;
            }
            else
            {
                return null;
            }
        }

        public bool UpdatePayment(PaymentEM model)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var payment = _context.Payments.Where(s => s.Id == model.Id).FirstOrDefault();
                if (payment != null)
                {
                    payment.PaymentDateTime = model.PaymentDateTime;
                    payment.PaymentStatus = model.PaymentStatus;
                    payment.TransactionId = model.TransactionId;
                    payment.UserId = model.UserId;
                    payment.Amount = model.Amount;
                    payment.OrderId = model.OrderId;
                }
                _context.SaveChanges();
                ts.Complete();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PaymentOM> GetUserPayments(string userId)
        {
            var userPayments = _context.Payments.Where(e => e.UserId == userId).ToList();
            if (userPayments.Count() == 0)
            {
                return null;
            }
            List<PaymentOM> cartDto = new List<PaymentOM>();
            foreach (var Item in userPayments)
            {
                cartDto.Add(new PaymentOM
                {
                    Id = Item.Id,
                    PaymentDate = Item.PaymentDateTime,
                    Amount = Item.Amount,
                    TransactionId = Item.TransactionId,
                    PaymentStatus = Item.PaymentStatus,
                    UserId = Item.UserId,
                    UserName = Item.User.FirstName + Item.User.LastName

                });
            }

            return cartDto;
        }


        public bool AddCardDetail(CardIM model)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var _model = new CardDetail
                {
                    Id = model.Id,
                    CardHolderName = model.CardHolderName,
                    CardNumber = model.CardNumber,
                    ExpiryDate = model.ExpiryDate,
                    UserId = model.UserId,
                   
                    CVV = model.CVV,
                };
                _context.CardDetails.Add(_model);

                int bit = _context.SaveChanges();
                ts.Complete();
                if (bit > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteCardDetail(string cardId)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var carddetail = _context.CardDetails.Where(s => s.Id == cardId).FirstOrDefault();
                if (carddetail == null)
                {
                    return false;
                }
                _context.CardDetails.Remove(carddetail);

                _context.SaveChanges();
                ts.Complete();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateCardDetails(CardEM model)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var carddetail = _context.CardDetails.Where(s => s.Id == model.Id).FirstOrDefault();
                if (carddetail != null)
                {
                    carddetail.ExpiryDate = model.ExpiryDate;
                    carddetail.CardNumber = model.CardNumber;
                    carddetail.CardHolderName = model.CardHolderName;
                    carddetail.UserId = model.UserId;
                    carddetail.CVV = model.CVV;
                    
                }
                _context.SaveChanges();
                ts.Complete();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
