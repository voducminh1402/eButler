using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace DataAccess.Services
{
    public class HouseKeeperService
    {
        private HouseKeeperService() { }
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private readonly UserService userService = UserService.GetInstance;
        private static HouseKeeperService instance = null;
        private static readonly object instanceLock = new object();
        public static HouseKeeperService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new HouseKeeperService();
                    }
                    return instance;
                }
            }
        }
        public HouseKeeperService(eButlerContext context, UserService userService)
        {
            _context = context;
            this.userService = userService;
        }
        public HouseKeeper GetHouseKeeperById(string id)
        {
            return _context.HouseKeepers.FirstOrDefault(h => h.Id == id);
        }
        public IEnumerable<HouseKeeper> GetHouseKeepers()
        {
            return _context.HouseKeepers.ToList();
        }
        public HouseKeeper CreateHouseKeepper(string id)
        {
            var houseKeeper = new HouseKeeper();
            houseKeeper.Id = id;
            var entity = _context.HouseKeepers.Add(houseKeeper);
            _context.SaveChanges();
            return entity.Entity;
        }
        public HouseKeeper UpdateHouseKeeper(HouseKeeper house, User u)
        {
            var housekeeper = GetHouseKeeperById(house.Id);
            var user = userService.GetUserById(u.Id);
            if(housekeeper != null && user != null)
            {
                user.Email = u.Email;
                housekeeper.FullName = house.FullName;
                housekeeper.Phone = house.Phone;
                housekeeper.Gender = house.Gender;
                var entity = _context.HouseKeepers.Update(housekeeper);
                _context.Users.Update(user);
                _context.SaveChanges();
                return entity.Entity;
            } else
            {
                throw new Exception("User is not exist.");
            }
        }
    }
}
