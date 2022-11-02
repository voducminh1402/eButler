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
    public class IndexModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public IndexModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public IList<HouseKeeper> HouseKeeper { get;set; }

        public async Task OnGetAsync()
        {
            HouseKeeper = await _context.HouseKeepers
                .Include(h => h.IdNavigation).ToListAsync();
        }
    }
}
