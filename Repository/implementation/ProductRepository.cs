using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;
using System.Data.SqlClient;
using System.Linq;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ContextClass _context;
        public ProductRepository( ContextClass context)
        {
            _context = context;

        }

        public Products Create(Products product)
        {
            var Product1 = _context.Products.Add(product);
            _context.SaveChanges();
            return product;


        }

        public List<Products> GetAll()
        {
            var products = _context.Products
                .Include(a => a.Category)
                .Where(a => a.IsAvailable == true && a.IsDeleted == false && a.IsAvailable == true)
                .ToList();
            return products;
        }

        public List<Products> GetByAvailability()
        {
            var products = _context.Products
                .Include(a => a.Category)
                .Where(a =>  a.IsDeleted == false)
                .ToList();
            return products;
        }

        public List<Products> GetByCartegoryId(string CartegoryId)
        {
            
            var product = _context.Products
                .Include(a => a.Category)
                .Where(u => u.CategoryId == CartegoryId && u.IsDeleted == false && u.IsAvailable == true )
                .ToList();
            return product;
        }

        public List<Products> GetByDescription(string Description)
        {
            var product = _context.Products
                .Include(a => a.Category)
                .Where(u => u.Description == Description && u.IsDeleted == false && u.IsAvailable == true)
                .ToList();
            return product;
        }

        public List<Products> GetByPrice(double Price)
        {
            var product = _context.Products
                 .Include(a => a.Category)
                 .Where(a => a.Price <= Price && a.IsDeleted == false && a.IsAvailable == true)
                 .ToList();
           return product;
        }

        public Products GetById(string Id)
        {
            
            var product = _context.Products
                 .Include(a => a.Category)
                 .FirstOrDefault(a => a.Id == Id && a.IsDeleted == false );
            return product;
        }

        public Products Update(Products product)
        {
            var updatedProperty = _context.Products.Update(product).Entity;
            _context.SaveChanges();
            return updatedProperty;
        }

    }
}