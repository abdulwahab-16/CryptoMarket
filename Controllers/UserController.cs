using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;
using Azure;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Service.Interface;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Service.Implementation;

namespace RealEstate_Mvc_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IProductService _ProductService;


        public UserController(IUserService userService,IStaffService staffService,IProductService productService)
        {
            _userService = userService;
            _staffService = staffService;   
            _ProductService = productService;
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SuperAdminDashBord()
        {
            var getAllProduct = _ProductService.GetByAvailability();
            var product = getAllProduct.Data;
            return View(product);
        }

        [HttpPost]
        public IActionResult Login(UserLoginRequestModel userLoginRequestModel)
        {
            var login = _userService.Login(userLoginRequestModel);
            if (login.Data == null)
            {
                ViewBag.Message = login.Message;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, login.Data.Email),
                new Claim(ClaimTypes.Name, login.Data.FullName),
                new Claim(ClaimTypes.NameIdentifier, login.Data.Id),
                new Claim(ClaimTypes.Role, login.Data.RoleName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            if (login.Data.RoleName == "Customer")
            {
                return RedirectToAction("GetAllProduct", "Product");
            }
            return RedirectToAction("SuperAdminDashBord", "User");
        }
        public IActionResult createUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createUser(UserRequestModel user )
        {
            var createUser = _userService.Create( user);
            if (createUser.Data == null)
            {
                ViewBag.Message = createUser.Message;
                return View();

            }
            return RedirectToAction("UserDetails", new { email = createUser.Data.Email });

        }
        [HttpGet]
        public IActionResult UserDetails(string email)
        {

            var getByEmail = _userService.Get(email);
            if (getByEmail.Data == null)
            {
                ViewBag.Message = getByEmail.Message;
                return RedirectToAction("GetAllUser");
            }

            return RedirectToAction("createStaff", "Staff");
        }
        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteUser(string Email)
        {

            var delete = _userService.Delete(Email);
            if (delete.Data == null)
            {
                ViewBag.Message = delete.Message;
                return RedirectToAction("SuperAdminDashBord");
            }
            return View(delete);
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var getAllUser = _userService.GetAll();
            return View(getAllUser);
        }


        [HttpGet]
        public IActionResult GetByRoleId(string Id)
        {
            var getByRoleId = _userService.GetByRoleId(Id);
            if (getByRoleId.Data == null)
            {
                ViewBag.Message = getByRoleId.Message;
                return RedirectToAction("GetAllUser");
            }
            return View(getByRoleId);

        }
        [HttpGet]
        public IActionResult GetUserByEmail(string Email)
        {
            var getByEmail = _userService.Get(Email);
            if (getByEmail.Data == null)
            {
                ViewBag.Message = getByEmail.Message;
                return RedirectToAction("GetAllUser");
            }
            return View(getByEmail);

        }
        [HttpGet]
        public IActionResult UpdateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateUser(UserRequestModel userRequestModel)
        {
            var update = _userService.Update(userRequestModel);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllUser");
            }
            return View(update);
        }
    }
}
