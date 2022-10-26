using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace eButler.Pages
{
    public class LoginGoogleModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            if (User.Identities.Any(identity => identity.IsAuthenticated))
            {
                return RedirectToPage("/Index");
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Index" : returnUrl;
            var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
            return new ChallengeResult("google", authenticationProperties);
        }
    }
}
