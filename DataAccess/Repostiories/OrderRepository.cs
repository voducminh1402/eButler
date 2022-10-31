using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrderById(string id);
        Order CreateOrder(Order order);
        string OrderStatus(Order order);
    }
    public class OrderRepository : IOrderRepository
    {
        public Order CreateOrder(Order order)
        {
            return OrderService.GetInstance.CreateOrder(order);
        }

        public Order GetOrderById(string id)
        {
            return OrderService.GetInstance.GetOrderById(id);
        }

        public List<Order> GetOrders()
        {
            return OrderService.GetInstance.GetOrders();
        }

        public string OrderStatus(Order order)
        {
            return OrderService.GetInstance.OrderStatus(order);
        }
    }
}
