using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace eButler.Pages.Admin.ProductSupliers
{
    public class IndexModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public IndexModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public IList<ProductSupplier> ProductSupplier { get;set; }

        public async Task OnGetAsync()
        {
            ProductSupplier = await _context.ProductSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier).ToListAsync();
        }
    }
}
