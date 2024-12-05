using RealEstateMvc.Models.Entities;
using System;
using System.Data;
using RealEstateMvc.Models.Enum;
using System.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Repository.Interface;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ContextClass _context;
        public TransactionRepository(ContextClass context)
        {
            _context = context;
        }

        public bool Check(string RefNumber)
        {
            var exist = _context.Transactions.Any(c => c.RefNumber == RefNumber && c.IsDeleted == false);
            return exist;
        }

        public Transaction Create(Transaction transaction)
        {
            var transaction1 = _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public List<Transaction> GetAll()
        {
            var transactions = _context.Transactions
                 .Include(a => a.Products)
                 .Include(a => a.Customer)
                  .Where(a => a.IsDeleted == false)
                 .ToList();
            return transactions;
        }

        public List<Transaction> GetByCustomerEmail(string Email)
        {
            var transaction = _context.Transactions
                .Include(a => a.Products)
                .Include(a => a.Customer)
                .Where(a => a.Email == Email && a.IsDeleted == false)
                .ToList();
            return transaction;
        }

        public List<Transaction> GetByCustomerId(string Id)
        {
            var transaction = _context.Transactions
                .Include(a => a.Products)
                .Include(a => a.Customer)
                .Where(a => a.CustomerId == Id)
                .ToList();
            return transaction;
        }


        public List<Transaction> GetByProductId(string ProductId)
        {
            var transaction = _context.Transactions
                .Include(a => a.Products)
                .Include(a => a.Customer)
                .Where(a => a.ProductsId == ProductId)
                .ToList();
            return transaction;
        }

        public Transaction GetByRefNumber(string RefNumber)
        {
            var transaction = _context.Transactions
                .Include(a => a.Products)
            .Include(a => a.Customer)
                .FirstOrDefault(a => a.RefNumber == RefNumber);
            return transaction;
        }

        public Transaction Update(Transaction transaction)
        {
            var transaction1 = _context.Transactions.Update(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }
}