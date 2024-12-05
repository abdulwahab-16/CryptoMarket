namespace RealEstate_Mvc_.Dtos
{
    public class UserRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; }
        public string? FullName { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
    }
    public class UserLoginRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class UserResponseModel
    {
        public string Email { get; set; } = default!;
        public string? FullName { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string RoleName { get; set; } = default!;
        public string? RoleDescription { get; set; }



    }
}
