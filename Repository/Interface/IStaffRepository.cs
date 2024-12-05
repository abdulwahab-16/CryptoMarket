using RealEstate_Mvc_.Models.Entities;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface IStaffRepository
    {
        bool Check(string Email);
        Staff Create(Staff Staff);
        Staff GetById(string Id);
        Staff GetByEmail(string Email);
        Staff GetByTagNumber(string StaffNumber);
        List<Staff> GetAll();
        Staff Update(Staff Staff);
    }
}
