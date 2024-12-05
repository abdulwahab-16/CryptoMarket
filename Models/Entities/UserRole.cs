using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Models.Entities
{
    public class UserRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = default!;
        public string RoleId { get; set; } = default!;
        public User ?User { get; set; }
        public Role ?Role { get; set; }
    }
}
