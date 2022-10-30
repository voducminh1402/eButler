using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class ShippingService
    {
        private ShippingService() { }
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static ShippingService instance = null;
        private static readonly object instanceLock = new object();
        public static ShippingService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new ShippingService(); 
                    }
                    return instance;
                }
            }
        }
        public Shipping GetShippingById(string id)
        {
            return _context.Shippings.FirstOrDefault(s => s.Id == id);
        }
        public IEnumerable<Shipping> GetShippingsForHouseKeeper(string id)
        {
            return _context.Shippings.Where(s => s.HouseKeeperId == id);
        }
        public Shipping CreateShipping(string status, string distric, string city, string country, string phone, string note, string houseKeeperId)
        {
            var shipping = new Shipping()
            {
                Id = Guid.NewGuid().ToString(),
                Status = status,
                Distric = distric,
                City = city,
                Country = country,
                Phone = phone,
                Note = note,
                HouseKeeperId = houseKeeperId
            };
            var entity = _context.Shippings.Add(shipping);
            _context.SaveChanges();
            return entity.Entity;
        }
        public Shipping UpdateShipping(Shipping shipping)
        {
            var s = GetShippingById(shipping.Id);
            if(s == null)
            {
                s.Phone = shipping.Phone;
                s.City = shipping.City;
                s.Status = shipping.Status;
                s.Address = shipping.Address;
                s.Distric = shipping.Distric;
                s.Country = shipping.Country;
                s.Note = shipping.Note;
                var entity = _context.Shippings.Update(s);
                _context.SaveChanges();
                return entity.Entity;
            }
            else
            {
                throw new Exception("Shipping address is not exist!");
            }
        }
    }
}
