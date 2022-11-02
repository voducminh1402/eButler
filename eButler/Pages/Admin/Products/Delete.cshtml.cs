using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;

namespace eButler.Pages.Admin.Products
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public DeleteModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);

            if (Product != null)
            {
                try
                {
                    _context.Products.Remove(Product);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    return Page();
                    ex = new Exception("Can't detele this Product because it have ProductSupplier!!!");
                }
                
            }

            return RedirectToPage("./Index");
        }
    }
}
