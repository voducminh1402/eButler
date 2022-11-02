using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLogic.Models;

namespace eButler.Pages.Admin.ProductSupliers
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
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
        ViewData["ProductName"] = new SelectList(_context.Products, "Id", "Name");
        ViewData["SupplierName"] = new SelectList(_context.Suppliers, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public ProductSupplier ProductSupplier { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProductSuppliers.Add(ProductSupplier);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
