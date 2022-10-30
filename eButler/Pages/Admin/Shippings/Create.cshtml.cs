using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLogic.Models;

namespace eButler.Pages.Admin.Shippings
{
    public class CreateModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public CreateModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["HouseKeeperId"] = new SelectList(_context.HouseKeepers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Shipping Shipping { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shippings.Add(Shipping);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
