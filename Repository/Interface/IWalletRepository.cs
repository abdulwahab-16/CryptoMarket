using RealEstate_Mvc_.Models.Entities;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface IWalletRepository
    {
        bool Check(string Id);
        Wallet GetById(string Id);
        Wallet Create(Wallet wallet);
        Wallet GetByCustomerEmail(string Email);
        Wallet GetByPassKey(string Id);
        List<Wallet> GetAll();
        Wallet Update(Wallet wallet);
    }
}
