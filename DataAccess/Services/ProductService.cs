using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class ProductService
    {
        private ProductService() { }
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static ProductService instance = null;
        private static readonly object instanceLock = new object();
        public static ProductService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new ProductService();
                    }
                    return instance;
                }
            }
        }

        public ProductService(eButlerContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductSupplier> GetAllProductSupplierByProductID(string id)
        {
            var product = _context.ProductSuppliers.Where(u => u.ProductId.Equals(id)).ToList();
            return product;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public ProductSupplier GetProductSupplierByProductID(string id)
        {
            var ps = _context.ProductSuppliers.Where(u => u.ProductId.Equals(id)).FirstOrDefault();
            return ps;
        }

        public void CreateNewProductSupplier(ProductSupplier ps)
        {
            _context.ProductSuppliers.Add(ps);
            _context.SaveChanges();
        }

        public void AddNewSupplier(Supplier sup)
        {
            _context.Suppliers.Add(sup);
            _context.SaveChanges();
        }

        public void DeleteProduct(String id)
        {
            var ps = GetProductSupplierByProductID(id);
            _context.ProductSuppliers.Remove(ps);
            _context.SaveChanges();
        }

        public Supplier getSupplierByID(String id)
        {
            var sup = _context.Suppliers.Where(u => u.Id.Equals(id)).FirstOrDefault();
            return sup;
        }
    }
}
