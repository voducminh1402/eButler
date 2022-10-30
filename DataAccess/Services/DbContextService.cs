using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class DbContextService
    {
        private static eButlerContext instance = null;
        private static readonly object instanceLock = new object();
        public static eButlerContext GetDbContext
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new eButlerContext();
                    }
                    return instance;
                }
            }
        }
    }
}
