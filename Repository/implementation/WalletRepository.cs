using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Models.Entities;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;
using System.Security.Cryptography;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ContextClass _context;
        public WalletRepository(ContextClass context)
        {
            _context = context;
        }
        public bool Check(string Id)
        {
            var exist = _context.Wallets.Any(c => c.Id == Id && c.IsDisabled == false);
            return exist;
        }

        public Wallet Create(Wallet wallet)
        {
            var wallet1 = _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet;
        }

        public List<Wallet> GetAll()
        {
            var wallet = _context.Wallets
                 .Where(a => a.IsDisabled == false)
                .ToList();
            return wallet;
        }

        public Wallet GetByCustomerEmail(string Email)
        {
            var wallet = _context.Wallets
               .FirstOrDefault(a => a.CustomerEmail == Email && a.IsDisabled == false);
            return wallet;
        }

        public Wallet GetById(string Id)
        {
            var wallet = _context.Wallets
              .FirstOrDefault(a => a.Id == Id && a.IsDisabled == false);
            return wallet;
        }

        public Wallet GetByPassKey(string Id)
        {
            var wallet = _context.Wallets
             .FirstOrDefault(a => a.PassCode == Id && a.IsDisabled == false);
            return wallet;
        }

        public Wallet Update(Wallet wallet)
        {
            var wallet1 = _context.Wallets.Update(wallet);
            _context.SaveChanges();
            return wallet;
        }
    }
}
