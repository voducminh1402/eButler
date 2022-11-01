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
        readonly IProductRepository _productRepository;
        public CategoryModel(BusinessLogic.Models.eButlerContext context, IProductSupplierRepository productSupplierRepository, ISupplierRepository supplierRepository, IProductRepository productRepository)
        {
            _context = context;
            _productSupplierRepository = productSupplierRepository;
            _supplierRepository = supplierRepository;
            _productRepository=productRepository;
        }
        [BindProperty]
        public IEnumerable<ProductSupplier> listProductSupplier { get;set; }
        [BindProperty]
        public IEnumerable<Supplier> listSupplier { get; set; }
        [BindProperty]
        public string SupplierID { get; set; }
        [BindProperty]
        public IEnumerable<Product> listProduct { get; set; }
        [BindProperty]
        public string ProductID { get; set; }

        public IActionResult OnPost()
        {

            listSupplier = _supplierRepository.GetAllSupplier();
            listProduct = _productRepository.GetAllProduct();

            if (string.IsNullOrEmpty(SupplierID) && string.IsNullOrEmpty(ProductID))
            {
                listProductSupplier = _productSupplierRepository.GetAllProductSupplier();
                return Page();
            }
            else if (SupplierID != null && string.IsNullOrEmpty(ProductID))
            {
                listProductSupplier = _productSupplierRepository.GetProductSupplierBySupplierID(SupplierID);
                return Page();
            }else if (ProductID != null && string.IsNullOrEmpty(SupplierID))
            {
                listProductSupplier = _productSupplierRepository.GetProductSupplierByProductID(ProductID);
                return Page();
            }
            
            return Page();
        }

        public IActionResult OnGet()
        {

            //listSupplier = _supplierRepository.GetAllSupplier();

            //if (string.IsNullOrEmpty(SupplierID))
            //{
            //    listProductSupplier = _productSupplierRepository.GetAllProductSupplier();
            //    return Page();
            //}
            //else
            //{
            //    listProductSupplier = _productSupplierRepository.GetProductSupplierBySupplierID(SupplierID);
            //    return Page();
            //}

            listSupplier = _supplierRepository.GetAllSupplier();
            listProduct = _productRepository.GetAllProduct();

            if (string.IsNullOrEmpty(SupplierID) && string.IsNullOrEmpty(ProductID))
            {
                listProductSupplier = _productSupplierRepository.GetAllProductSupplier();
                return Page();
            }
            else if (SupplierID != null && string.IsNullOrEmpty(ProductID))
            {
                listProductSupplier = _productSupplierRepository.GetProductSupplierBySupplierID(SupplierID);
                return Page();
            }
            else if (ProductID != null && string.IsNullOrEmpty(SupplierID))
            {
                listProductSupplier = _productSupplierRepository.GetProductSupplierByProductID(ProductID);
                return Page();
            }

            return Page();

        }
    }
}
