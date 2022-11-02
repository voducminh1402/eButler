using BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class OrderService
    {
        private OrderService() { }
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static OrderService instance = null;
        private static readonly object instanceLock = new object();
        public static OrderService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new OrderService();
                    }
                    return instance;
                }
            }
        }

        public Order CreateOrder(Order order)
        {
            Order orderCreated = _context.Orders.Add(order).Entity;
            _context.SaveChanges();

            return orderCreated;
        }

        public List<Order> GetOrderBySupplier(string supplierId)
        {
            List<ProductSupplier> productSuppliers = _context.ProductSuppliers.Where(x => x.SupplierId.Equals(supplierId)).ToList();
            List<OrderDetail> orderDetails = _context.OrderDetails.ToList();
            List<OrderDetail> tmpList = new List<OrderDetail>();

            foreach(var item in productSuppliers)
            {
                foreach (var orderDetail in orderDetails)
                {
                    if (item.Id.Equals(orderDetail.ProductSupplierId))
                    {
                        tmpList.Add(orderDetail);
                    }
                }
            }

            List<OrderDetail> orderDetailTmp = tmpList.GroupBy(x => x.OrderId).Select(o => o.First()).ToList();

            List<Order> orderReturn = new List<Order>();

            foreach (var orderDetail in orderDetailTmp)
            {
                orderReturn.Add(_context.Orders.Where(x => x.Id.Equals(orderDetail.OrderId)).FirstOrDefault());
            }

            return orderReturn;
        }

        public string OrderStatus(Order order)
        {
            Shipping shipping = _context.Shippings.Where(x => x.Id.Equals(order.Id)).FirstOrDefault();

            return shipping.Status;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = _context.Orders.ToList();

            return orders;
        }

        public Order GetOrderById(string id)
        {
            Order order = _context.Orders.Where(x => x.Id.Equals(id)).FirstOrDefault();
            //order.Transactions = _context.Transactions.Where(x => x.OrderId.Equals(id)).ToList();

            return order;
        }
    }
}
