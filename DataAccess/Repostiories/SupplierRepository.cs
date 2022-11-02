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
        IEnumerable<Supplier> GetSupplierBySupplierID(string id);
        IEnumerable<Supplier> GetAllSupplier();

        void AddNewSupplier(Supplier supplier);

        Supplier getSupplierByID(string id); 
    }

    public class SupplierRepository : ISupplierRepository
    {
        public void AddNewSupplier(Supplier supplier) => SupplierService.GetInstance.AddNewSupplier(supplier);

        public IEnumerable<Supplier> GetAllSupplier()
        {
            return SupplierService.GetInstance.GetAllSupplier();
        }

        public Supplier getSupplierByID(string id)
        {
            return SupplierService.GetInstance.getSupplierByID(id);
        }

        public IEnumerable<Supplier> GetSupplierBySupplierID(string id)
        {
            return SupplierService.GetInstance.GetSupplierBySupplierID(id);
        }
    }
}
