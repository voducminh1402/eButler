using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IShippingRepository
    {
        Shipping GetShippingById(string id);
        IEnumerable<Shipping> GetShippingsForHouseKeeper(string id);
        Shipping CreateShipping(string status, string distric, string city, string country, string phone, string note, string houseKeeperId);
        Shipping UpdateShipping(Shipping shipping);
        Shipping CreateShipping(Shipping shipping);
        void ChangeStatus(string id, string status);
    }
    public class ShippingRepository : IShippingRepository
    {
        public void ChangeStatus(string id, string status)
        {
            ShippingService.GetInstance.ChangeStatus(id, status);
        }

        public Shipping CreateShipping(string status, string distric, string city, string country, string phone, string note, string houseKeeperId)
        {
            return ShippingService.GetInstance.CreateShipping(status, distric, city, country, phone, note, houseKeeperId);
        }

        public Shipping CreateShipping(Shipping shipping)
        {
            return ShippingService.GetInstance.CreateShipping(shipping);
        }

        public Shipping GetShippingById(string id)
        {
            return ShippingService.GetInstance.GetShippingById(id);
        }

        public IEnumerable<Shipping> GetShippingsForHouseKeeper(string id)
        {
            return ShippingService.GetInstance.GetShippingsForHouseKeeper(id);
        }

        public Shipping UpdateShipping(Shipping shipping)
        {
            return ShippingService.GetInstance.UpdateShipping(shipping);
        }
    }
}
