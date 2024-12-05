using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface IRoleRepository
    {
        bool Check(string Name);
        Role Create(Role role);
        Role GetByName(string Name);
        Role Update(Role role);

        List<Role> GetAll();
    }
}