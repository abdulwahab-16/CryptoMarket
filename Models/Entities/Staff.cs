using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Models.Entities
{
    public class Staff : Person
    {
        public string StaffTagNumber { get; set; } = default!;
        public string? RoleName { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}
