using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eButler.Pages
{
    public class LoginGoogleModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            if (User.Identities.Any(identity => identity.IsAuthenticated))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var items = new Dictionary<string, string>();
                items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties(items);

                await HttpContext.SignInAsync(claimsPrincipal, properties);
                return RedirectToPage("/Index");
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Index" : returnUrl;
            var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
            return new ChallengeResult("google", authenticationProperties);
        }
    }
}
