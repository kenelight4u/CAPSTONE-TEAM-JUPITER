using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using JupiterCapstone.ViewModels.EditModels;
using JupiterCapstone.ViewModels.InputModels;
using JupiterCapstone.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace JupiterCapstone.Services
{
  
    
    public class PaymentAccess : IPayment
    {
        private ApplicationDbContext _context;
        public PaymentAccess()
        {
            _context = new ApplicationDbContext();
        }
        
        public bool AddPayment(PaymentIM model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeletePayment(string paymentId)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
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
                using (TransactionScope ts = new TransactionScope())
                {
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
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void PayNow()
        {
            throw new NotImplementedException();
        }
    }
}
