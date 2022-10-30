using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace eButler.Pages.Admin.Shippings
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public DeleteModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shipping Shipping { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shipping = await _context.Shippings
                .Include(s => s.HouseKeeper).FirstOrDefaultAsync(m => m.Id == id);

            if (Shipping == null)
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

            Shipping = await _context.Shippings.FindAsync(id);

            if (Shipping != null)
            {
                _context.Shippings.Remove(Shipping);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
