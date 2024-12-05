using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface ICustomerRepository
    {
        bool Check(string Email);
        Customer Create(Customer customer);
        Customer GetByEmail(string Email);
        Customer GetById(string Id);
        List<Customer> GetAll();
        Customer Update(Customer customer);
    }
}