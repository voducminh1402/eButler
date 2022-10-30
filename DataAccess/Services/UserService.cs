﻿using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class UserService
    {
        private UserService() { }
        
        private readonly eButlerContext _context = DbContextService.GetDbContext;
        private static UserService instance = null;
        private static readonly object instanceLock = new object();
        public static UserService GetInstance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance is null)
                    {
                        instance = new UserService();
                    }
                    return instance;
                }
            }
        }
        public UserService(eButlerContext context)
        {
            _context = context;
        }
        public User GetUserById(string nameIdentifier)
        {
            var user = _context.Users.Where(u => u.Id == nameIdentifier || u.UserName == nameIdentifier).FirstOrDefault();
            return user;
        }
        public User GetUserByUserName(string username)
        {
            var user = _context.Users.Where(u => u.UserName == username).FirstOrDefault();
            return user;
        }
        public User Login(string userName, string password)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }
        public User AddNewUser(List<Claim> claims)
        {
            User user1 = GetUserById(claims.GetClaim(ClaimTypes.NameIdentifier));
            if (user1 == null)
            {
                var user = new User();
                user.Id = claims.GetClaim(ClaimTypes.NameIdentifier);
                user.UserName = claims.GetClaim("username") == null ? claims.GetClaim("name") : claims.GetClaim("username");
                user.Email = claims.GetClaim(ClaimTypes.Email) == null ? "google" : claims.GetClaim(ClaimTypes.Email);
                user.RoleId = "2";
                user.Password = "123";
                user.IsActive = true;
                var entity = _context.Users.Add(user);
                _context.SaveChanges();
                return entity.Entity;
            }
            else
            {
                throw new Exception("User is already exist");
            }
        }

        public User AddNewUser(User user)
        {
            if (GetUserByUserName(user.UserName) != null)
                throw new Exception("User name is already exist!");
            user.Id = Guid.NewGuid().ToString();
            user.IsActive = true;
            user.Email = user.UserName;
            user.RoleId = "1";
            user.IsSystemAdmin = false;
            var entity = _context.Users.Add(user);
            _context.SaveChanges();
            return entity.Entity;
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }

    public static class Extensions
    {
        public static string GetClaim(this List<Claim> claims, string name)
        {

            return claims.FirstOrDefault(c => c.Type == name)?.Value;

        }
    }
}