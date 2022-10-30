using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSupplierBySupplierID(String id);
        IEnumerable<Supplier> GetAllSupplier();
    }

    public class SupplierRepository : ISupplierRepository
    {
        public IEnumerable<Supplier> GetAllSupplier()
        {
            return SupplierService.GetInstance.GetAllSupplier();
        }

        public IEnumerable<Supplier> GetSupplierBySupplierID(String id)
        {
            return SupplierService.GetInstance.GetSupplierBySupplierID(id);
        }
    }
}
