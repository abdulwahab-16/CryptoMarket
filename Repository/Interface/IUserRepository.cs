using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface IUserRepository
    {
        bool Check(string email);
        User Create(User user);
        User Get(string email);
        List<User> GetByRoleId(string Id);
        List<User> GetAll();
        User Update(User user);

    }
}