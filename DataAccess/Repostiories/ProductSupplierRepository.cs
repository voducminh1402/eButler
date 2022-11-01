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
        IEnumerable<ProductSupplier> GetProductSupplierByProductID(String id);
        void deleteProductSupplierById(string id);
        ProductSupplier GetProductSupplierByProductId(String id);


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

        public IEnumerable<ProductSupplier> GetProductSupplierByProductID(string id)
        {
            return ProductSupplierService.GetInstance.GetProductSupplierByProductID(id);
        }

        public void deleteProductSupplierById(string id) => ProductSupplierService.GetInstance.deleteProductSupplierById(id);

        public ProductSupplier GetProductSupplierByProductId(string id)
        {
            return ProductSupplierService.GetInstance.GetProductSupplierByProductId(id);
        }
    }
}
