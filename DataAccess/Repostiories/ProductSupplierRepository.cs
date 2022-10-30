using BusinessLogic.Models;
using System;
using DataAccess.Services;
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
    }
}
