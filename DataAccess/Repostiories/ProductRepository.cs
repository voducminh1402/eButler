using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IProductRepository
    {
        IEnumerable<ProductSupplier> GetAllProductSupplierByProductID(String id);
        IEnumerable<Product> GetAllProduct();

    }
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProduct()
        {
            return ProductService.GetInstance.GetAllProduct();
        }

        public IEnumerable<ProductSupplier> GetAllProductSupplierByProductID(string id)
        {
            return ProductService.GetInstance.GetAllProductSupplierByProductID(id);
        }
    }
}
