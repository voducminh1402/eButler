using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using DataAccess.Repostiories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace eButler.Pages.Admin.ProductSupliers
{
    public class IndexModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;
        readonly IProductSupplierRepository _productSupplierRepository;
        [BindProperty]
        public User User1 { get; set; }
        public IndexModel(BusinessLogic.Models.eButlerContext context, IProductSupplierRepository productSupplierRepository)
        {
            _context = context;
            _productSupplierRepository = productSupplierRepository;
        }
        public IEnumerable<ProductSupplier> listProductSupplier { get; set; }
        public IList<ProductSupplier> ProductSupplier { get;set; }

        public async Task OnGetAsync()
        {
            if (User.IsInRole("Supplier"))
            {
                //Chua lay duoc data theo SupplierID de CRUD
                //listProductSupplier = _productSupplierRepository.GetProductSupplierBySupplierID("3");
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var id = result.Principal.Claims.FirstOrDefault(x => x.Type == "supplierID").Value;
                ProductSupplier = await _context.ProductSuppliers.Include(p => p.Product)
               .Include(p => p.Supplier).Where(p => p.SupplierId.Equals(id)).ToListAsync();
            }
            else
            {
                ProductSupplier = await _context.ProductSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier).ToListAsync();
            }
            
        }
    }
}
