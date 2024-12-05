using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;
using System.Data.SqlClient;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ContextClass _context;
        public RoleRepository(ContextClass context)
        {
            _context = context;
        }

        public bool Check(string Name)
        {
            var exist = _context.Roles.Any(u => u.Name == Name && u.IsDeleted == false);
            return exist;
        }

        public Role Create(Role role)
        {
            var role1 = _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public List<Role> GetAll()
        {
            var roles = _context.Roles
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.User)
                   .Where(a => a.IsDeleted == false)
                .ToList();
            return roles;
        }

        public Role GetByName(string Name)
        {
            var role = _context.Roles
                 .Include(a => a.UserRoles)
                 .ThenInclude(a => a.User)
                 .FirstOrDefault(u => u.Name == Name && u.IsDeleted == false);
            return role;
        }

        public Role Update(Role role)
        {
            var update = _context.Roles.Update(role);
            _context.SaveChanges();
            return role;
        }
    }
}