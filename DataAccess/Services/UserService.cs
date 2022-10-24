using BusinessLogic.Models;
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
        private readonly eButlerContext _context;
        public UserService(eButlerContext context)
        {
            _context = context;
        }
        public User GetUserById(string nameIdentifier)
        {
            var user = _context.Users.Where(u => u.Id == nameIdentifier).FirstOrDefault();
            return user;
        }
        public User AddNewUser(List<Claim> claims)
        {
            var user = new User();
            user.Id = claims.GetClaim(ClaimTypes.NameIdentifier);
            user.UserName = claims.GetClaim("username") == null ? "google" : claims.GetClaim("username");
            user.Email = claims.GetClaim(ClaimTypes.Email);
            user.RoleId = "2";
            user.Password = "123";
            user.IsActive = true;
            var entity = _context.Users.Add(user);
            _context.SaveChanges();
            return entity.Entity;
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
