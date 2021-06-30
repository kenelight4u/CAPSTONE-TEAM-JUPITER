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
    public class OrderAccess : IOrder
    {
        private readonly ApplicationDbContext _context;
        public OrderAccess(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddOrder(OrderIM model)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var _model = new Order
                {
                    Id = model.Id,
                    TotalPrice = model.TotalPrice,
                    OrderDate = model.OrderDate,
                    Status = model.Status,
                    PaymentType = model.PaymentType,

                };
                _context.Orders.Add(_model);

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

        public bool DeleteOrder(string orderId)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var order = _context.Orders.Where(s => s.Id == orderId).FirstOrDefault();
                if (order == null)
                {
                    return false;
                }
                _context.Orders.Remove(order);

                _context.SaveChanges();
                ts.Complete();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public OrderOM GetOrderById(string orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                var resp = new OrderOM
                {
                    Id = order.Id,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    PaymentType = order.PaymentType,
                };
                return resp;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateOrder(OrderEM model)
        {
            try
            {
                using TransactionScope ts = new TransactionScope();
                var order = _context.Orders.Where(s => s.Id == model.Id).FirstOrDefault();
                if (order != null)
                {

                    order.TotalPrice = model.TotalPrice;
                    order.OrderDate = model.OrderDate;
                    order.Status = model.Status;
                    order.PaymentType = model.PaymentType;
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

        public void CancelOrder(List<Order> order)
        {
            throw new NotImplementedException();
        }
    }
}
