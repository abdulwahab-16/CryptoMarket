using RealEstateMvc.Models.Enum;

namespace RealEstateMvc.Models.Entities
{
    public abstract class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public long PhoneNumber { get; set; } 
        public int Age { get; set; } 
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; } 
        public bool IsDeleted { get; set; }
        public DateTime Dob {get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCrated { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
        public DateTime? DateDeleted { get; set; } 



    }
}