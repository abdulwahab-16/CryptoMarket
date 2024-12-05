using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Dtos
{
    public class StaffRequestModel
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public long PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string RoleName { get; set; } = default!;
        public string RoleDescription { get; set; } = default!;
        public IFormFile? ProfilePicture { get; set; }

    }
    public class StaffResponseModel
    {
        public string StaffTagNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public long PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public IFormFile? ProfilePicture { get; set; }


    }
}
