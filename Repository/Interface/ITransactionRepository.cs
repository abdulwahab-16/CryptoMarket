using RealEstateMvc.Models.Entities;


namespace RealEstate_Mvc_.Repository.Interface
{
    public interface ITransactionRepository
    {
        bool Check(string RefNumber);
        Transaction Create(Transaction transaction);
        Transaction GetByRefNumber(string RefNumber);
        List<Transaction> GetByCustomerEmail(string Email);
        List<Transaction> GetByCustomerId(string Id);
        List<Transaction> GetByProductId(string ProductId);
        List<Transaction> GetAll();
        Transaction Update(Transaction transaction);






    }
}