using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class RoleService
    {
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        public static RoleService instance = null;
        private static readonly object instanceLock = new object();
        private RoleService() { }
        public static RoleService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new RoleService();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
