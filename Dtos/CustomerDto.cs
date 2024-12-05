using RealEstate_Mvc_.Models.Entities;
using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Dtos
{
    public class CustomerRequestModel
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public long PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? PassCode { get; set; }


    }
    public class CustomerResponseModel
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public long PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; }
        public double Wallet { get; set; } = 0;
        public List<TransactionResponseModel> Transactions { get; set; } = new List<TransactionResponseModel>();
        public string? ProfilePicture { get; set; }

    }
}
