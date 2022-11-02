using BusinessLogic.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
    }
    public class RoleRepository : IRoleRepository
    {
        public IEnumerable<Role> GetRoles()
        {
            return RoleService.GetInstance.GetRoles();
        }
    }
}
