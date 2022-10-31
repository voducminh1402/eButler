using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eButler.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public IUserRepository userRepository;
        public LoginModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void OnGet(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            User user = userRepository.Login(User.UserName, User.Password);
            if (user == null)
            {
                TempData["Error"] = "User name or Password is not valid!";
                return Page();
            }
            HttpContext.Session.SetString("LOGIN_USER", JsonConvert.SerializeObject(user));
            ViewData["ReturnUrl"] = returnUrl;
            var claims = new List<Claim>();
            claims.Add(new Claim("username", User.UserName));
            claims.Add(new Claim("supplierID", user.Id));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, User.UserName == null ? User.Id : User.UserName));
            if (user.IsSystemAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }
            if (user.RoleId.Equals("3"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Supplier"));
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
