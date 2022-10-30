using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PayPal.Api;

namespace eButler.Configuration
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        //Constructor
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = "AeZzFFDI12-OdnxuHI3lE0eftxjdu4ehVzDlH6eqnkSeu5R76MOY_tCmOWdi1CptzKrAlTwQQjm2NG6A";
            ClientSecret = "EB7Mmy9pMXK-k0tCUB2qP1Y_EDza1bdIAaHDNiLITxdzUf8x9CDD0Q2KSt1quSNw7xwRLIP80FyKaf-N";
        }
        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        private static string GetAccessToken()
        {
            // getting accesstocken from paypal
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}
