using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class SupplierService
    {
        private SupplierService() { }

        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static SupplierService instance = null;
        private static readonly object instanceLock = new object();
        public static SupplierService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new SupplierService();
                    }
                    return instance;
                }
            }
        }

        public SupplierService(eButlerContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetSupplierBySupplierID(String id)
        {
            var productSupplier = _context.Suppliers.Where(u => u.Id.Equals(id)).ToList();
            return productSupplier;
        }

        public IEnumerable<Supplier> GetAllSupplier()
        {
            return _context.Suppliers.ToList();
        }

        public ProductSupplier GetProductSupplierByPSID(String id)
        {
            var ps = _context.ProductSuppliers.Where(u => u.Id.Equals(id)).FirstOrDefault();
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

        public void DeleteProductSupplier(ProductSupplier ps)
        {
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
