
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;
using System.Data.SqlClient;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class CartegoryRepository : ICartegoryRepository
    {
        private readonly ContextClass _context;
        public CartegoryRepository(ContextClass context)
        {
            _context = context;
        }
        public bool Check(string Name)
        {
            var exist = _context.Categories.Any(u => u.Name == Name && u.IsDeleted == false);
            return exist;
        }

        public Category Create(Category cartegory)
        {
            var category = _context.Categories.Add(cartegory);
            _context.SaveChanges();
            return cartegory;
        }

        public List<Category> GetAll()
        {
            var categories = _context.Categories
                  .Include(a => a.Products)
                   .Where(a => a.IsDeleted == false)
                .ToList();
            return categories;

        }

        public Category GetByName(string Name)
        {
            var category = _context.Categories
            .Include(a => a.Products)
                .FirstOrDefault(u => u.Name == Name && u.IsDeleted == false);
            return category;
        }
        public Category GetById(string Id)
        {
            var category = _context.Categories
                  .Include(a => a.Products)
                  .FirstOrDefault(a => a.Id == Id && a.IsDeleted == false);
            return category;
        }

        public Category Update(Category cartegory)
        {
            var category = _context.Categories.Update(cartegory);
            _context.SaveChanges();
            return cartegory;
        }
    }
}