using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;

namespace eButler.Pages.Admin.HouseKeepers
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public DetailsModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
