using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextClass _context;
        public UserRepository(ContextClass context)
        {
            _context = context;
        }

        public bool Check(string email)
        {
            var exist = _context.Users.Any(u => u.Email == email && u.IsDeleted == false);
            return exist;
        }

        public User Create(User user)
        {
            var user1 = _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Get(string email)
        {
            var user = _context.Users
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .FirstOrDefault(a => a.Email == email && a.IsDeleted == false);
            return user;
        }

        public List<User> GetAll()
        {
            var users = _context.Users
            .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                   .Where(a => a.IsDeleted == false)
               .ToList();
            return users;
        }

        public List<User> GetByRoleId(string Id)
        {
            var user = _context.Users
              .Include(a => a.UserRoles)
              .ThenInclude(a => a.Role)
             .Where(a => a.UserRoles.Any(ur => ur.RoleId == Id) && a.IsDeleted == false)
             .ToList();
            return user;

        }

        public User Update(User user)
        {
            var user1 = _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}