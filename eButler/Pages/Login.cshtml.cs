using BusinessLogic.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eButler.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public eButlerContext _context;
        public LoginModel(eButlerContext context)
        {
            _context = context;
        }

        public void OnGet(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.UserName && u.Password == User.Password);
            if (user == null)
            {
                TempData["Error"] = "User name or Password is not valid!";
                return Page();
            }
            ViewData["ReturnUrl"] = returnUrl;
            var claims = new List<Claim>();
            claims.Add(new Claim("username", User.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, User.UserName));
            if(user.RoleId == "1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            } 
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var items = new Dictionary<string, string>();
            items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties(items);
                
            await HttpContext.SignInAsync(claimsPrincipal, properties);

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/Index";
            }

            return Redirect(returnUrl);
        }
    }
}
