using JupiterCapstone.Models;
using JupiterCapstone.ViewModels.EditModels;
using JupiterCapstone.ViewModels.InputModels;
using JupiterCapstone.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IOrder
    {
        OrderOM GetOrderById(string orderId);
        bool UpdateOrder(OrderEM model);
        bool DeleteOrder(string OrderId);
        bool AddOrder(OrderIM model);
        void CancelOrder(List<Order> order);
    }
}
