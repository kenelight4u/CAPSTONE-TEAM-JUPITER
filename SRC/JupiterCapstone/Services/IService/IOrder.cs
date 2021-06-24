using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    interface IOrder
    {
        void AddOrders(List<Order> order);
        void CancelOrder(List<Order> order);
    }
}
