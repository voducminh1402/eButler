using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface ICheckOutRepository
    {
        bool GetCheckOutStatus();
        void SetCheckOutStatus(bool status);
    }
    public class CheckOutRepository : ICheckOutRepository
    {
        public bool GetCheckOutStatus()
        {
            return CheckOutService.GetInstance.GetCheckOutSuccess;
        }

        public void SetCheckOutStatus(bool status)
        {
            CheckOutService.GetInstance.GetCheckOutSuccess = status;
        }
    }
}
