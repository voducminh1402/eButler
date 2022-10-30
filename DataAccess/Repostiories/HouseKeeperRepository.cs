using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Services;

namespace DataAccess.Repostiories
{
    public interface IHouseKeeperRepository
    {
        HouseKeeper GetHouseKeeperById(string id);
        IEnumerable<HouseKeeper> GetHouseKeepers();
        HouseKeeper CreateHouseKeepper(string id);
        HouseKeeper UpdateHouseKeeper(HouseKeeper house, User user, string id);
    }
    public class HouseKeeperRepository : IHouseKeeperRepository
    {
        public HouseKeeper CreateHouseKeepper(string id)
        {
            return HouseKeeperService.GetInstance.CreateHouseKeepper(id);
        }

        public HouseKeeper GetHouseKeeperById(string id)
        {
            return HouseKeeperService.GetInstance.GetHouseKeeperById(id);
        }

        public IEnumerable<HouseKeeper> GetHouseKeepers()
        {
            return HouseKeeperService.GetInstance.GetHouseKeepers();
        }

        public HouseKeeper UpdateHouseKeeper(HouseKeeper house, User user, string id)
        {
            return HouseKeeperService.GetInstance.UpdateHouseKeeper(house, user, id);
        }
    }
}
