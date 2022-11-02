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
        public ISupplierRepository supplierRepository;
        Supplier Sup;
        public LoginModel(IUserRepository userRepository, ISupplierRepository supplierRepository)
        {
            this.userRepository = userRepository;
            this.supplierRepository=supplierRepository;
        }

        public void OnGet(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            User user = userRepository.Login(User.UserName, User.Password);
            Sup = null;
            Sup = supplierRepository.getSupplierByID(user.Id);
            if (user == null)
            {
                TempData["Error"] = "User name or Password is not valid!";
                return Page();
            }
            HttpContext.Session.SetString("LOGIN_USER", JsonConvert.SerializeObject(user));
            ViewData["ReturnUrl"] = returnUrl;
            var claims = new List<Claim>();
            claims.Add(new Claim("username", User.UserName));
            if (Sup != null)
            {
                claims.Add(new Claim("supplierID", user.Id));
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, User.UserName == null ? User.Id : User.UserName));
            if (user.IsSystemAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            if (Sup != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Supplier"));
            }
            if (user.RoleId.Equals("2"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var items = new Dictionary<string, string>();
            items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties(items);
                
            await HttpContext.SignInAsync(claimsPrincipal, properties);

            if (user.RoleId.Equals("1") || user.RoleId.Equals("3"))
            {
                returnUrl = "/Admin/Index";
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/Index";
            }

            return Redirect(returnUrl);
        }
    }
}
