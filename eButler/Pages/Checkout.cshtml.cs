using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PayPal.Api;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace eButler.Pages
{
    //public class Transaction
    //{
    //    public string intent { get; set; }
    //    public Dictionary<string, dynamic> purchase_units { get; set; }
    //}
    //public class Amount
    //{
    //    public string currency_code { get; set; }
    //    public double value { get; set; }
    //}

    public partial class Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("links")]
        public Link[] Links { get; set; }
    }

    public partial class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }
    
    [Authorize(Policy = "User")]
    public class CheckoutModel : PageModel
    {
        private const string URL = "https://api-m.sandbox.paypal.com/v2/checkout/orders";

        private string yourusername = "AeZzFFDI12-OdnxuHI3lE0eftxjdu4ehVzDlH6eqnkSeu5R76MOY_tCmOWdi1CptzKrAlTwQQjm2NG6A";
        private string yourpwd = "EB7Mmy9pMXK-k0tCUB2qP1Y_EDza1bdIAaHDNiLITxdzUf8x9CDD0Q2KSt1quSNw7xwRLIP80FyKaf-N";
        private string newUrl { get; set; }
        private StringContent data { get; set; }
        public List<CartItem> Cart { get; set; }

        [BindProperty]
        public Shipping Shipping { get; set; }

        public CheckoutModel()
        {
            Cart = new List<CartItem>();
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

        public void OnGet()
        {
            Cart = GetCartItem();
        }
        public async Task<IActionResult> OnPostAsync(Shipping Shipping, double amount)
        {
            data = new StringContent("{\"intent\": \"AUTHORIZE\",\"purchase_units\": [{\"amount\": {\"currency_code\": \"USD\",\"value\": \""+ amount +"\"}}],\"application_context\": {\"brand_name\": \"eButler\",\"shipping_preference\": \"NO_SHIPPING\",\"return_url\": \"https://localhost:5001/checkoutsuccess?status=COMPLETED\"}}", Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            var authen = new AuthenticationHeaderValue(
        "Basic", Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
               $"{yourusername}:{yourpwd}")));
            client.DefaultRequestHeaders.Authorization = 
    authen;
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            var response = await client.PostAsync(URL, data);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            var result_string = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(result_string);
            var links = result.Links;
            //var links = (IList<Link>)JsonConvert.DeserializeObject(result["links"].ToString());
            foreach (var link in links)
            {
                if (link.Rel == "approve")
                {
                    newUrl = link.Href;
                }
            }
            HttpContext.Session.SetString("ShippingInfo", JsonConvert.SerializeObject(Shipping));
            HttpContext.Session.SetString("url", newUrl);
            HttpContext.Session.SetString("authen", "Basic " + Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
               $"{yourusername}:{yourpwd}")));
            TempData["url"] = newUrl;
            TempData["orderId"] = result.Id;
            TempData["authen"] = "Basic " + Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
               $"{yourusername}:{yourpwd}"));
            return Page();
            //result = JsonConvert.DeserializeObject(result_string);
        }
    }
}
