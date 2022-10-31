using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eButler.Pages.Admin.Order
{
    public class OrderResponse
    {
        public BusinessLogic.Models.Order Order { get; set; }
        public string Status { get; set; }
    }
    public class IndexModel : PageModel
    {
        public readonly IOrderRepository _orderRepository;
        public List<BusinessLogic.Models.Order> Orders { get; set; }
        public List<OrderResponse> OrderResponses { get; set; }

        public IndexModel(IOrderRepository orderRepository)
        {
            OrderResponses = new List<OrderResponse>();
            _orderRepository = orderRepository;
        }

        public void OnGet()
        {
            Orders = _orderRepository.GetOrders();

            foreach (var item in Orders)
            {
                OrderResponses.Add(new OrderResponse { Order = item, Status = _orderRepository.OrderStatus(item) });
            }
        }
    }
}
