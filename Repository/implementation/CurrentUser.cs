using System.Security.Claims;
using RealEstate_Mvc_.Repository.Interface;

namespace RealEstate_Mvc_.Repository.implementation
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContext;
        public CurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetCurrentUser()
        {
            return _httpContext.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
        }
    }
}