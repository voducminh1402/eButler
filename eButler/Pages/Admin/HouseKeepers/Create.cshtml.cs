using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;

namespace eButler.Pages.Admin.HouseKeepers
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public CreateModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public HouseKeeper HouseKeeper { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.HouseKeepers.Add(HouseKeeper);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
