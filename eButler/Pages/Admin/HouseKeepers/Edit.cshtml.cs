using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;

namespace eButler.Pages.Admin.HouseKeepers
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public EditModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HouseKeeper HouseKeeper { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HouseKeeper = await _context.HouseKeepers
                .Include(h => h.IdNavigation).FirstOrDefaultAsync(m => m.Id == id);

            if (HouseKeeper == null)
            {
                return NotFound();
            }
           ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(HouseKeeper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseKeeperExists(HouseKeeper.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool HouseKeeperExists(string id)
        {
            return _context.HouseKeepers.Any(e => e.Id == id);
        }
    }
}
