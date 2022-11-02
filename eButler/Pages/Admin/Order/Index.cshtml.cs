using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eButler.Pages.Admin.Order
{
    public class OrderResponse
    {
        public BusinessLogic.Models.Order Order { get; set; }
        public string Status { get; set; }
    }

    [Authorize(Policy = "AdSup")]
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
            var session = HttpContext.Session;
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(session.GetString("LOGIN_USER"));

            if (user.RoleId.Equals("3"))
            {
                Orders = _orderRepository.GetOrderBySupplier(user.Id);

                foreach (var item in Orders)
                {
                    OrderResponses.Add(new OrderResponse { Order = item, Status = _orderRepository.OrderStatus(item) });
                }
            }
            else
            {
                Orders = _orderRepository.GetOrders();

                foreach (var item in Orders)
                {
                    OrderResponses.Add(new OrderResponse { Order = item, Status = _orderRepository.OrderStatus(item) });
                }
            }
        }
    }
}
