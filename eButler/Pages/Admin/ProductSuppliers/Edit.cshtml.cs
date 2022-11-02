﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace eButler.Pages.Admin.ProductSupliers
{
    public class EditModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;

        public EditModel(BusinessLogic.Models.eButlerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductSupplier ProductSupplier { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductSupplier = await _context.ProductSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier).FirstOrDefaultAsync(m => m.Id == id);

            if (ProductSupplier == null)
            {
                return NotFound();
            }
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
           ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            ViewData["ProductName"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["SupplierName"] = new SelectList(_context.Suppliers, "Id", "Name");
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

            _context.Attach(ProductSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSupplierExists(ProductSupplier.Id))
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

        private bool ProductSupplierExists(string id)
        {
            return _context.ProductSuppliers.Any(e => e.Id == id);
        }
    }
}
