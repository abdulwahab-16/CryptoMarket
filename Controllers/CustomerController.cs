using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.implementation;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using System.Security.Claims;

namespace RealEstate_Mvc_.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICurrentUser _currentUser;

        public CustomerController(ICustomerService customerService, ICurrentUser currentUser)
        {
            _customerService = customerService;
            _currentUser = currentUser;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMoneyToWallet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMoneyToWallet( double amount, string passcode)
        {

            
            var addMoneyToWallet = _customerService.AddMoneyToWallet(amount, passcode);
            if (addMoneyToWallet.Data == null)
            {
                ViewBag.Message = addMoneyToWallet.Message;
                return View();

            }
            ViewBag.Message = addMoneyToWallet.Message;
            if (User.FindFirst(ClaimTypes.Role) != null && User.FindFirst(ClaimTypes.Role).Value == "Customer")
            {
                return RedirectToAction("GetAllProduct", "Product");
            }
            return RedirectToAction("SuperAdminDashBord", "User");
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerRequestModel customer)
        {

            
           var createCustomer = _customerService.Create(customer);
            if (createCustomer.Data == null)
            {
                ViewBag.Message = createCustomer.Message;
                return View();

            }
            return RedirectToAction("Login","User");
            
        }
        [HttpGet]
        public IActionResult DeleteCustomer()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult DeleteCustomer(string Email)
        {

            var delete = _customerService.Delete(Email);
            if (delete.Data == null)
            {
                ViewBag.Message = delete.Message;
                return RedirectToAction("GetAllCustomer");
            }
            return View(delete);
        }


        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var getAllCustomer= _customerService.GetAll();
            return View(getAllCustomer.Data);
            
        }

        [HttpGet]
        public IActionResult GetCustomerByEmail()
        {
            var email = _currentUser.GetCurrentUser();

            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Message = "Please enter a customer Email.";
                return View();
            }
            var getByEmail= _customerService.GetByEmail(email);
            if (getByEmail.Data == null)
            {
                ViewBag.Message = getByEmail.Message;
                return RedirectToAction("GetAllCustomer");
            }
            return View(getByEmail.Data);


        }
        [HttpGet]
        public IActionResult GetCustomerById(string Id)
        {
            var getById= _customerService.GetById(Id);
            if (getById.Data == null)
            {
                ViewBag.Message = getById.Message;
                return View();
            }
            return View(getById.Data);

        }
        [HttpGet]
        public IActionResult UpdateCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateCustomer(CustomerRequestModel customer)
        {
            var update = _customerService.Update(customer);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllCustomer");
            }

            TempData["Message"] = update.Message;
            if (User.FindFirst(ClaimTypes.Role) != null && User.FindFirst(ClaimTypes.Role).Value == "Customer")
            {
                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("SuperAdminDashBord", "User");
        }


    }
}
