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
        ProductSupplier GetProductSupplierById(string id);
    }

    public class ProductSupplierRepository : IProductSupplierRepository
    {
        public ProductSupplier GetProductSupplierById(string id)
        {
            return ProductSupplierService.GetInstance.GetProductById(id);
        }
    }
}
