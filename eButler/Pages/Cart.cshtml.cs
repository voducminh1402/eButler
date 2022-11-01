using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eButler.Pages
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public ProductSupplier Product { get; set; }
    }

    public class CartModel : PageModel
    {
        [BindProperty]
        public ProductSupplier ProductSupplier { get; set; }
        public List<CartItem> Cart { get; set; }
        public string Error { get; set; }
        public IProductSupplierRepository _productSupplierRepository;

        public CartModel(IProductSupplierRepository productSupplierRepository)
        {
            Error = null;
            Cart = new List<CartItem>();
            _productSupplierRepository = productSupplierRepository;
        }

        public List<CartItem> GetCartItem()
        {
            var session = HttpContext.Session;
            string cart = session.GetString("CART");
            if (cart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }
            return Cart;
        }

        public void ResetCart(List<CartItem> newCart)
        {
            var session = HttpContext.Session;
            string currentCart = JsonConvert.SerializeObject(newCart);
            session.SetString("CART", currentCart);
        }

        public void ValidateCartQuantity(string productId, int quantity, int currentDBQuantity)
        {
            List<CartItem> currentCart = GetCartItem();
            var currenProductInCart = currentCart.Find(x => x.Product.Id.Equals(productId));

            int currentInCart = currenProductInCart == null ? 0 : currenProductInCart.Quantity;

            if (currentInCart + quantity > currentDBQuantity)
            {
                throw new Exception($"Total quantity of this product is : {currentDBQuantity}");
            }
        }

        public void AddToCart(string productId, int quantity)
        {
            List<CartItem> currentCart = GetCartItem();

            var cartItemCheck = currentCart.Find(x => x.Product.Id.Equals(productId));
            var productCheck = _productSupplierRepository.GetProductSupplierById(productId);

            if (cartItemCheck == null)
            {
                ValidateCartQuantity(productId, quantity, productCheck.Quantiy);
                currentCart.Add(new CartItem { Quantity = quantity, Product = productCheck });
            }
            else
            {
                ValidateCartQuantity(productId, quantity, productCheck.Quantiy);
                currentCart.Find(x => x.Product.Id.Equals(productId)).Quantity += quantity;
            }
            ResetCart(currentCart);
        }

        public void UpdateCart(string productId, int quantity)
        {
            List<CartItem> currentCart = GetCartItem();

            var cartItemCheck = currentCart.Find(x => x.Product.Id.Equals(productId));
            var productCheck = _productSupplierRepository.GetProductSupplierById(productId);

                ValidateCartQuantity(productId, quantity, productCheck.Quantiy);
                currentCart.Find(x => x.Product.Id.Equals(productId)).Quantity = quantity;

            ResetCart(currentCart);

        }

        public void RemoveItem(string productId)
        {
            List<CartItem> currentCart = GetCartItem();
            CartItem itemRemove = currentCart.Find(x => x.Product.Id.Equals(productId));
            currentCart.Remove(itemRemove);
            ResetCart(currentCart);
        }

        public void OnGet()
        {
            Cart = GetCartItem();
        }

        public IActionResult OnPost(string productId, string action, int quantity)
        {
            try
            {
                if (action != null && action.Equals("Remove")) {
                    RemoveItem(productId);
                }
                else if (action != null && action.Equals("Update"))
                {
                    UpdateCart(productId, quantity);
                }
                else
                {
                    AddToCart(productId, quantity);
                }
            }
            catch(Exception ex)
            {
                Error = ex.Message;
            }
            return RedirectToPage("/Cart");
        }
    }
}
