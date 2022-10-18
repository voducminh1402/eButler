using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace eButler.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public IndexModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public IList<Role> Role { get;set; }

        public async Task OnGetAsync()
        {
            Role = await _context.Roles.ToListAsync();
        }
    }
}
