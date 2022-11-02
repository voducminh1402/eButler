using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class OrderDetailService
    {
        private OrderDetailService() { }
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static OrderDetailService instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new OrderDetailService();
                    }
                    return instance;
                }
            }
        }

        public OrderDetail CreateOrderDetail(OrderDetail orderDetail)
        {
            OrderDetail orderDetailCreated = _context.OrderDetails.Add(orderDetail).Entity;
            ProductSupplier productSupplier = _context.ProductSuppliers.Where(x => x.Id.Equals(orderDetail.ProductSupplierId)).FirstOrDefault();
            productSupplier.Quantiy -= orderDetail.Quantity;
            _context.Update(productSupplier);
            _context.SaveChanges();

            return orderDetailCreated;
        }

        public List<OrderDetail> OrderDetailsByOrder(string orderId)
        {
            List<OrderDetail> orderDetails = _context.OrderDetails.Where(x => x.OrderId.Equals(orderId)).ToList();
            List<OrderDetail> returnList = new List<OrderDetail>();

            foreach (var item in orderDetails) 
            {
                item.ProductSupplier = _context.ProductSuppliers.Where(x => x.Id.Equals(item.ProductSupplierId)).FirstOrDefault();
                returnList.Add(item);
            }

            return returnList;
        }

        public List<OrderDetail> OrderDetailsByOrderAndSupplier(string supplierId, string orderId)
        {
            List<ProductSupplier> productSuppliers = _context.ProductSuppliers.Where(x => x.SupplierId.Equals(supplierId)).ToList();
            List<OrderDetail> orderDetails = _context.OrderDetails.ToList();
            List<OrderDetail> tmpList = new List<OrderDetail>();

            foreach (var item in productSuppliers)
            {
                foreach (var orderDetail in orderDetails)
                {
                    if (item.Id.Equals(orderDetail.ProductSupplierId))
                    {
                        tmpList.Add(orderDetail);
                    }
                }
            }

            List<OrderDetail> orderDetailTmp = tmpList.Where(u => u.OrderId.Equals(orderId)).GroupBy(x => x.OrderId).Select(o => o.First()).ToList();

            return orderDetailTmp;
        }
    }
}
