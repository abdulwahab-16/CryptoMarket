
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;
using System.Data.SqlClient;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ContextClass _context;
        public CustomerRepository(ContextClass context)
        {
            _context = context;
        }

        public bool Check(string email)
        {
            var exist = _context.Customers.Any(u => u.Email == email && u.IsDeleted == false);
            return exist;
        }

        public Customer Create(Customer customer)
        {
            var customer1 = _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public List<Customer> GetAll()
        {
            var customers = _context.Customers
                .Include(a => a.Transactions)
                   .Where(a => a.IsDeleted == false)
                .ToList();
            return customers;
        }

        public Customer GetByEmail(string Email)
        {
            var customers = _context.Customers
                .Include(a => a.Transactions)
                .FirstOrDefault(u => u.Email == Email && u.IsDeleted == false);
            return customers;
        }

        public Customer GetById(string Id)
        {
            var customers = _context.Customers
                .Include(a => a.Transactions)
                .FirstOrDefault(a => a.Id == Id && a.IsDeleted == false);
            return customers;
        }

        public Customer Update(Customer customer1)
        {
            var customer = _context.Customers.Update(customer1);
            _context.SaveChanges();
            return customer1;
        }
    }
}