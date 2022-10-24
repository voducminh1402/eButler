using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repostiories
{
    public interface IUserRepository
    {
        User GetUserById(string id);
        User CreateUser(User user);
        User AddNewUser(List<Claim> claims);
    }
    public class UserRepository : IUserRepository
    {
        public User AddNewUser(List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
