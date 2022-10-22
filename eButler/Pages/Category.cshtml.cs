using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace eButler.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public CategoryModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        public IList<ProductSupplier> listProductSupplier { get;set; }

        public async Task OnGetAsync()
        {
            listProductSupplier = await _context.ProductSuppliers.ToListAsync();
        }
    }
}
