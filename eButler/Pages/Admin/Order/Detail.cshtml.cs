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
    public class ResponseOrderDetail
    {
        public BusinessLogic.Models.Order Order { get; set; }
        public List<BusinessLogic.Models.OrderDetail> OrderDetails { get; set; }
        public BusinessLogic.Models.Shipping Shipping { get; set; }
    }

    [Authorize(Policy = "AdSup")]
    public class DetailModel : PageModel
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IOrderDetailRepository _orderDetailRepository;
        public readonly IShippingRepository _shippingRepository;

        public List<BusinessLogic.Models.Order> Orders { get; set; }
        public ResponseOrderDetail OrderResponses { get; set; }

        public DetailModel(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IShippingRepository shippingRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _shippingRepository = shippingRepository;
        }

        public void OnGet(string orderId)
        {
            var session = HttpContext.Session;
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(session.GetString("LOGIN_USER"));

            BusinessLogic.Models.Order order = _orderRepository.GetOrderById(orderId);
            BusinessLogic.Models.Shipping shipping = _shippingRepository.GetShippingById(orderId);
            List<BusinessLogic.Models.OrderDetail> orderDetails = new List<OrderDetail>();

            if (user.RoleId.Equals("3"))
            {
                orderDetails = _orderDetailRepository.GetOrderDetailBySupplier(user.Id, orderId);
            }
            else
            {
                orderDetails = _orderDetailRepository.GetOrderDetailsByOrder(orderId);
            }

            OrderResponses = new ResponseOrderDetail
            {
                Order = order,
                OrderDetails = orderDetails,
                Shipping = shipping
            };
        }

        public IActionResult OnPost(string id, string status)
        {
            _shippingRepository.ChangeStatus(id, status);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
