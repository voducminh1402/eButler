using eButler.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PayPal.Api;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

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
    
    public class CheckoutModel : PageModel
    {
        private const string URL = "https://api-m.sandbox.paypal.com/v2/checkout/orders";
        private string urlParameters = "?api_key=123";
        private string yourusername = "AeZzFFDI12-OdnxuHI3lE0eftxjdu4ehVzDlH6eqnkSeu5R76MOY_tCmOWdi1CptzKrAlTwQQjm2NG6A";
        private string yourpwd = "EB7Mmy9pMXK-k0tCUB2qP1Y_EDza1bdIAaHDNiLITxdzUf8x9CDD0Q2KSt1quSNw7xwRLIP80FyKaf-N";
        private string newUrl { get; set; }
        private StringContent data { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //Dictionary<string, dynamic> purchase_units = new Dictionary<string, dynamic>();
            //var amount = new Amount()
            //{
            //    currency_code = "USA",
            //    value = 1.00
            //};
            //purchase_units.Add("amount", amount);
            //var d = new Transaction()
            //{
            //    intent = "AUTHORIZE",
            //    purchase_units = purchase_units
            //};

            //var data = JsonConvert.SerializeObject(d);
            data = new StringContent("{\"intent\": \"AUTHORIZE\",\"purchase_units\": [{\"amount\": {\"currency_code\": \"USD\",\"value\": \"100.00\"}}],\"application_context\": {\"brand_name\": \"eButler\",\"shipping_preference\": \"NO_SHIPPING\",\"return_url\": \"https://localhost:5001/checkoutsuccess\"}}", Encoding.UTF8, "application/json");
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
            return RedirectToPage("/CheckoutSuccess");
            
            //result = JsonConvert.DeserializeObject(result_string);
        }
        //public IActionResult OnPost(string Cancel = null)
        //{
        //    //return RedirectToPage("/Index");
        //    //getting the apiContext
        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();
        //    try
        //    {
        //        //A resource representing a Payer that funds a payment Payment Method as paypal  
        //        //Payer Id will be returned when payment proceeds or click to pay  
        //        string payerId = Request.Query["PayerID"];
        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            //this section will be executed first because PayerID doesn't exist  
        //            //it is returned by the create function call of the payment class  
        //            // Creating a payment  
        //            // baseURL is the url on which paypal sendsback the data.  
        //            //string url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetFullUrl(Request);
        //            string baseURI = //Request.Uri.Scheme + 
        //                "https://localhost:5001" +
        //                //Request.Url.Authority + 
        //                "/CheckoutSuccess?";
        //            //here we are generating guid for storing the paymentID received in session  
        //            //which will be used in the payment execution  
        //            var guid = Convert.ToString((new Random()).Next(100000));
        //            //CreatePayment function gives us the payment approval url  
        //            //on which payer is redirected for paypal account payment  
        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
        //            //get links returned from paypal in response to Create function call  
        //            var links = createdPayment.links.GetEnumerator();
        //            string paypalRedirectUrl = null;
        //            while (links.MoveNext())
        //            {
        //                Links lnk = links.Current;
        //                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    //saving the payapalredirect URL to which user will be redirected for payment  
        //                    paypalRedirectUrl = lnk.href;
        //                }
        //            }
        //            // saving the paymentID in the key guid  
        //            HttpContext.Session.SetString(guid, createdPayment.id);
        //            return Redirect(paypalRedirectUrl);
        //        }
        //        else
        //        {
        //            // This function exectues after receving all parameters for the payment  
        //            var guid = Request.Query["guid"];
        //            var executedPayment = ExecutePayment(apiContext, payerId, HttpContext.Session.GetString(guid));
        //            //If executed payment failed then we will show payment failure message to user  
        //            if (executedPayment.state.ToLower() != "approved")
        //            {
        //                return RedirectToPage("/FailureView");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToPage("/FailureView");
        //    }
        //    //on successful payment, show success page to user.  
        //    return RedirectToPage("/Index");
        //}
        //private PayPal.Api.Payment payment;
        //private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution()
        //    {
        //        payer_id = payerId
        //    };
        //    this.payment = new Payment()
        //    {
        //        id = paymentId
        //    };
        //    return this.payment.Execute(apiContext, paymentExecution);
        //}
        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{
        //    //create itemlist and add item objects to it  
        //    var itemList = new ItemList()
        //    {
        //        items = new List<Item>()
        //    };
        //    //Adding Item Details like name, currency, price etc  
        //    itemList.items.Add(new Item()
        //    {
        //        name = "Item Name comes here",
        //        currency = "USD",
        //        price = "1",
        //        quantity = "1",
        //        sku = "sku"
        //    });
        //    var payer = new Payer()
        //    {
        //        payment_method = "paypal"
        //    };
        //    // Configure Redirect Urls here with RedirectUrls object  
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl + "&Cancel=true",
        //        return_url = redirectUrl
        //    };
        //    // Adding Tax, shipping and Subtotal details  
        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "1",
        //        subtotal = "1"
        //    };
        //    //Final amount with details  
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
        //        details = details
        //    };
        //    var transactionList = new List<Transaction>();
        //    // Adding description about the transaction  
        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Transaction description",
        //        invoice_number = "your generated invoice number", //Generate an Invoice No  
        //        amount = amount,
        //        item_list = itemList
        //    });
        //    this.payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };
        //    // Create a payment using a APIContext  
        //    return this.payment.Create(apiContext);
        //}
    }
}
