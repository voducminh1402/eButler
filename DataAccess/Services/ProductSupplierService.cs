using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class ProductSupplierService
    {
        private ProductSupplierService() { }
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static ProductSupplierService instance = null;
        private static readonly object instanceLock = new object();
        public static ProductSupplierService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new ProductSupplierService();
                    }
                    return instance;
                }
            }
        }

        public ProductSupplierService(eButlerContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductSupplier> GetProductSupplierBySupplierID(String id)
        {
            var productSupplier = _context.ProductSuppliers.Where(u => u.Id == id || u.SupplierId == id).ToList();
            return productSupplier;
        }

        public IEnumerable<ProductSupplier> GetAllProductSupplier()
        {
            return _context.ProductSuppliers.ToList();
        }

        public ProductSupplier GetProductById(string id)
        {
            var productSupplier = _context.ProductSuppliers.Where(x => x.Id.Equals(id)).FirstOrDefault();

            if (productSupplier != null)
            {
                return productSupplier;
            }

            return null;
        }
    }
}
