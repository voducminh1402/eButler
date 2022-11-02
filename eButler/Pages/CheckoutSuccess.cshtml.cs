using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace eButler.Pages
{

    [Authorize(Policy = "User")]
    public class CheckoutSuccessModel : PageModel
    {
        private readonly ICheckOutRepository checkOutRepository;
        private readonly IShippingRepository shippingRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public CheckoutSuccessModel(ICheckOutRepository checkOutRepository, IShippingRepository shippingRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.checkOutRepository = checkOutRepository;
            this.shippingRepository = shippingRepository;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public async Task<IActionResult> OnGetAsync(string status) 
        {
            if (status == "COMPLETED")
            {
                User currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("LOGIN_USER"));
                List<CartItem> cart = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("CART"));

                double total = 0;

                foreach (var item in cart)
                {
                    total += item.Quantity * item.Product.Price;
                }

                string orderId = Guid.NewGuid().ToString();

                checkOutRepository.SetCheckOutStatus(true);
                Shipping shipping = JsonConvert.DeserializeObject<Shipping>(HttpContext.Session.GetString("ShippingInfo"));
                string tmpAddress = shipping.Address;
                shipping.Distric = $"{shipping.Address}, {shipping.Distric}";
                shipping.Id = orderId;
                shipping.Status = "Pending";
                shipping.Address = tmpAddress;

                shippingRepository.CreateShipping(shipping);
                
                var orderReturn = orderRepository
                                        .CreateOrder(new BusinessLogic.Models.Order 
                                        { 
                                            Id = orderId, 
                                            TotalPrice = total, 
                                            UserId = currentUser.Id, 
                                            CreateDate = DateTime.Now 
                                        });

                if (orderReturn != null)
                {
                    foreach (var itemCart in cart)
                    {
                        OrderDetail orderDetailAdd = new OrderDetail
                        {
                            OrderId = orderId,
                            ProductSupplierId = itemCart.Product.ProductId,
                            Price = itemCart.Product.Price,
                            Quantity = itemCart.Quantity,
                            Discount = (double)itemCart.Product.Discount
                        };
                        orderDetailRepository.CreateOrderDetail(orderDetailAdd);
                    }
                }


            }
            return Page();
        }
        public IActionResult OnPost(string status)
        {
            return Page();
        }
    }
}
