using RealEstate_Mvc_.Models.Entities;

namespace RealEstate_Mvc_.Dtos
{
    public class RoleRequestModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    public class RoleResponseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
