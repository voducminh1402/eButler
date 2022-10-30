using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using DataAccess.Repostiories;

namespace eButler.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly BusinessLogic.Models.eButlerContext _context;
        readonly IProductSupplierRepository _productSupplierRepository;
        readonly ISupplierRepository _supplierRepository;
        public CategoryModel(BusinessLogic.Models.eButlerContext context, IProductSupplierRepository productSupplierRepository, ISupplierRepository supplierRepository)
        {
            _context = context;
            _productSupplierRepository = productSupplierRepository;
            _supplierRepository = supplierRepository;
        }
        [BindProperty]
        public IEnumerable<ProductSupplier> listProductSupplier { get;set; }
        [BindProperty]
        public IEnumerable<Supplier> listSupplier { get; set; }
        [BindProperty]
        public string SupplierID { get; set; }

        public IActionResult OnPost()
        {

            listSupplier = _supplierRepository.GetAllSupplier();

            if (string.IsNullOrEmpty(SupplierID))
            {
                listProductSupplier = _productSupplierRepository.GetAllProductSupplier();
                return Page();
            }
            else
            {
                listProductSupplier = _productSupplierRepository.GetProductSupplierBySupplierID(SupplierID);
                return Page();
            }

        }

        public IActionResult OnGet()
        {

            listSupplier = _supplierRepository.GetAllSupplier();

            if (string.IsNullOrEmpty(SupplierID))
            {
                listProductSupplier = _productSupplierRepository.GetAllProductSupplier();
                return Page();
            }
            else
            {
                listProductSupplier = _productSupplierRepository.GetProductSupplierBySupplierID(SupplierID);
                return Page();
            }
            
        }
    }
}
