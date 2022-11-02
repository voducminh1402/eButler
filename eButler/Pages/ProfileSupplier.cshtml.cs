using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace eButler.Pages
{
    public class ProfileSupplierModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public ProfileSupplierModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        

        public Supplier Supplier { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var SupId = result.Principal.Claims.FirstOrDefault(x => x.Type == "supplierID").Value;
            if (SupId == null)
            {
                return NotFound();
            }

            Supplier = await _context.Suppliers
                .Include(s => s.IdNavigation).FirstOrDefaultAsync(m => m.Id == SupId);

            if (Supplier == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
