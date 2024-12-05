using RealEstate_Mvc_.Models.Entities;

namespace RealEstateMvc.Models.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? CreatedBy { get; set; } 
        public DateTime DateCrated { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; } 
        public DateTime? DateDeleted { get; set; } 
        public string? FullName { get; set; }
        public bool IsDeleted { get; set; }
       public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}