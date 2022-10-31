using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IProductSupplierRepository
    {
        IEnumerable<ProductSupplier> GetProductSupplierBySupplierID(String id);
        IEnumerable<ProductSupplier> GetAllProductSupplier();
        ProductSupplier GetProductSupplierById(string id);
    }

    public class ProductSupplierRepository : IProductSupplierRepository
    {
        public IEnumerable<ProductSupplier> GetAllProductSupplier()
        {
            return ProductSupplierService.GetInstance.GetAllProductSupplier();
        }

        public IEnumerable<ProductSupplier> GetProductSupplierBySupplierID(String id)
        {
            return ProductSupplierService.GetInstance.GetProductSupplierBySupplierID(id);
        }
        
        public ProductSupplier GetProductSupplierById(string id)
        {
            return ProductSupplierService.GetInstance.GetProductById(id);
        }
    }
}
