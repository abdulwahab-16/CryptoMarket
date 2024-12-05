using RealEstate_Mvc_.Models.Entities;

namespace RealEstateMvc.Models.Entities
{
    public class Role
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? DeletedBy { get; set; } 
        public DateTime DateDeleted { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } 
        public DateTime? DateCrated { get; set; }
        public bool IsDeleted { get; set; }
       public  List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}