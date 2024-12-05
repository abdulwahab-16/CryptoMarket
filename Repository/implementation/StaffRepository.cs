using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Models.Entities;
using RealEstate_Mvc_.Repository.Interface;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ContextClass _context;
        public StaffRepository(ContextClass context)
        {
            _context = context;
        }
        public bool Check(string email)
        {
            var exist = _context.Staffs.Any(u => u.Email == email && u.IsDeleted == false);
            return exist;
        }

        public Staff Create(Staff Staff)
        {
            var staff = _context.Staffs.Add(Staff);
            _context.SaveChanges();
            return Staff;
        }

        public List<Staff> GetAll()
        {
            var staffs = _context.Staffs
                  .Where(a => a.IsDeleted == false)
                .ToList();
            return staffs;
        }

        public Staff GetByEmail(string Email)
        {
            var staff = _context.Staffs
                .FirstOrDefault(u => u.Email == Email && u.IsDeleted == false);
            return staff;
        }

        public Staff GetById(string Id)
        {
            var staff = _context.Staffs
                .FirstOrDefault(a => a.Id == Id && a.IsDeleted == false);
            return staff;
        }

        public Staff GetByTagNumber(string StaffNumber)
        {
            var staff = _context.Staffs
                .FirstOrDefault(a => a.StaffTagNumber == StaffNumber && a.IsDeleted == false);
            return staff;
        }

        public Staff Update(Staff Staff)
        {
            var staff = _context.Staffs.Update(Staff);
            _context.SaveChanges();
            return Staff;
        }
    }
}
