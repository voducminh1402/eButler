using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IOrderDetailRepository
    {
        OrderDetail CreateOrderDetail(OrderDetail orderDetail);
        List<OrderDetail> GetOrderDetailsByOrder(string orderId);
    }

    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail CreateOrderDetail(OrderDetail orderDetail)
        {
            return OrderDetailService.GetInstance.CreateOrderDetail(orderDetail);
        }

        public List<OrderDetail> GetOrderDetailsByOrder(string orderId)
        {
            return OrderDetailService.GetInstance.OrderDetailsByOrder(orderId);
        }
    }
}
