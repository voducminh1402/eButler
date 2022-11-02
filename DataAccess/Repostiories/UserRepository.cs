using BusinessLogic.Models;
using DataAccess.Services;
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
        User AddNewUser(User user);
        User AddNewUser(List<Claim> claims);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetUsersWithRole();
        User Login(string userName, string password);
        User GetUserByUserName(string username);
        User AddNewSupplier(User supplier);
        User GetUserByIdWithRole(string id);
        bool DeleteUser(string id);
        User UpdateUser(User user);
    }
    public class UserRepository : IUserRepository
    {
        public User AddNewUser(List<Claim> claims)
        {
            return UserService.GetInstance.AddNewUser(claims);
        }

        public User AddNewUser(User user)
        {
            return UserService.GetInstance.AddNewUser(user);
        }

        public User AddNewSupplier(User supplier)
        {
            return UserService.GetInstance.AddNewSupplier(supplier);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UserService.GetInstance.GetUsers();
        }

        public User GetUserById(string id)
        {
            return UserService.GetInstance.GetUserById(id);
        }

        public User GetUserByUserName(string username)
        {
            return UserService.GetInstance.GetUserByUserName(username);
        }

        public User Login(string userName, string password)
        {
            return UserService.GetInstance.Login(userName, password);
        }

        public IEnumerable<User> GetUsersWithRole()
        {
            return UserService.GetInstance.GetUsersWithRole();
        }

        public User GetUserByIdWithRole(string id)
        {
            return UserService.GetInstance.GetUserByIdWithRole(id);
        }

        public bool DeleteUser(string id)
        {
            return UserService.GetInstance.DeleteUser(id);
        }

        public User UpdateUser(User user)
        {
            return UserService.GetInstance.UpdateUser(user);
        }
    }
}
