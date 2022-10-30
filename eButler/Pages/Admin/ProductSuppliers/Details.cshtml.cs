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
    public class DetailsModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public DetailsModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public ProductSupplier ProductSupplier { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductSupplier = await _context.ProductSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier).FirstOrDefaultAsync(m => m.Id == id);

            if (ProductSupplier == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
