using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class CheckOutService
    {
        private CheckOutService() { }
        private static CheckOutService instance = null;
        private static readonly object lockObject = new object();
        private bool checkOutSuccess;
        public static CheckOutService GetInstance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new CheckOutService();
                }
                return instance;
            }
        }
        public bool GetCheckOutSuccess
        {
            get
            {
                return checkOutSuccess;
            }
            set
            {
                checkOutSuccess = value;
            }
        }
    }
}
