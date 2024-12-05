using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Implementation;
using RealEstate_Mvc_.Service.Interface;
using RealEstate_Mvc_.ViewModel;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;
using System.Net.NetworkInformation;
using System.Security.Claims;


namespace RealEstate_Mvc_.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _ProductService;
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerService _customerService;
        private readonly ICartegoryService _CartegoryService;

        public ProductController(IProductService ProductService, ICartegoryService cartegoryService, ICurrentUser currentUser, ICustomerService customerService)
        {
            _ProductService = ProductService;
            _CartegoryService = cartegoryService;
            _currentUser = currentUser;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var categories = _CartegoryService.GetAll();

            if (categories == null || categories.Data == null || !categories.Data.Any())
            {
                TempData["Error"] = "No categories available.";
                return View("Error");
            }

            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            return View(new ProductRequestModel());
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductRequestModel product)
        {


        

            var createProduct = _ProductService.Create(product);
            if (createProduct.Data == null)
            {
                ViewBag.Message = createProduct.Message;
                ViewBag.Categories = new SelectList(_CartegoryService.GetAll().Data, "Id", "Name");
                return View(product);
            }

            return RedirectToAction("SuperAdminDashBord", "User");

        }

        [HttpGet]
        public IActionResult DeleteProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteProduct(string Id)
        {

            var delete = _ProductService.Delete(Id);
            if (delete.Data == null)
            {
                ViewBag.Message = delete.Message;
                return RedirectToAction("GetAllProduct");
            }
            return View(delete);
        }
        

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var getAllProduct = _ProductService.GetAll();
            var products = getAllProduct.Data;
            var categories = _CartegoryService.GetAll();
            var categoriess = categories.Data;
            var current = _currentUser.GetCurrentUser();
            var cust = _customerService.GetByEmail(current);
            // Assuming you have a method to get categories
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            ViewBag.image = cust.Data.ProfilePicture;
            ViewBag.UserName = cust.Data.FirstName;
            var a = ViewBag.image = cust.Data.ProfilePicture;



            var viewModel = new ProductViewModel
            {
                product = products,
                Categories = categoriess
            };
            return View(viewModel);
        }
        //[HttpGet]
        //public IActionResult GetProductById()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetProductById(string Id)
        {
            var getProductById = _ProductService.GetById(Id);
            if (getProductById.Data == null)
            {
                ViewBag.Message = getProductById.Message;
                return RedirectToAction("GetAllProduct");
            }
            return View(getProductById.Data);

        }
        //[HttpGet]

        //public IActionResult GetPropertyByLocation()
        //{
        //    return View();
        //}
  


       
        //[HttpGet]
        //public IActionResult GetPropertyByPrice()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult GetProductByPrice(double price)
        {

            var current = _currentUser.GetCurrentUser();
            var cust = _customerService.GetByEmail(current);
            ViewBag.UserName = cust.Data.LastName;
            ViewBag.image = cust.Data.ProfilePicture;
            var getProductByPrice = _ProductService.GetByPrice(price);
            var products = getProductByPrice.Data;
            var categories = _CartegoryService.GetAll();
            var categoriess = categories.Data;

            var viewModel = new ProductViewModel
            {
                product = products,
                Categories = categoriess
            };
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            if (getProductByPrice.Data == null)
            {
                ViewBag.Message = getProductByPrice.Message;
                return View();
            }
            return View(viewModel);

        }

        //[HttpGet]
        //public IActionResult GetPropertyByCartegoryId()
        //{
        //    var categories = _CartegoryService.GetAll();
        //    ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetProductByCartegoryId(String CategoryId)
        {

            var current = _currentUser.GetCurrentUser();
            var cust = _customerService.GetByEmail(current);
            ViewBag.UserName = cust.Data.LastName;
            ViewBag.image = cust.Data.ProfilePicture;
            var getProductByCategoryId = _ProductService.GetByCartegoryId(CategoryId);
            var products = getProductByCategoryId.Data;
            var categories = _CartegoryService.GetAll();
            var categoriess = categories.Data;

            var viewModel = new ProductViewModel
            {
                product = products,
                Categories = categoriess
            };
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
            if (getProductByCategoryId.Data == null)
            {
                ViewBag.Message = "No products found under this category.";
                return View(ViewBag.Message = "No product found under this category.");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetByAvailability()
        {
            var current = _currentUser.GetCurrentUser();
            var cust = _customerService.GetByEmail(current);
            ViewBag.UserName = cust.Data.LastName;
            ViewBag.image = cust.Data.ProfilePicture;
            var getProductByAvailability = _ProductService.GetByAvailability();
            if (getProductByAvailability.Data == null)
            {
                ViewBag.Message = getProductByAvailability.Message;
                return RedirectToAction("GetAllProduct");
            }
            return View(getProductByAvailability);

        }
        [HttpGet]
        public IActionResult GetProductByDescription(string Description)
        {
            var current = _currentUser.GetCurrentUser();
            var cust = _customerService.GetByEmail(current);
            ViewBag.UserName = cust.Data.LastName;
            ViewBag.image = cust.Data.ProfilePicture;
            var getProductByDescription = _ProductService.GetByDescription(Description);
            var categories = _CartegoryService.GetAll();
            var categoriess = categories.Data;
            var products = getProductByDescription.Data;
            // Assuming you have a method to get categories
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");

            var viewModel = new ProductViewModel
            {
                product = products,
                Categories = categoriess
            };

            if (getProductByDescription.Data == null)
            {
                ViewBag.Message = getProductByDescription.Message;
                return RedirectToAction("GetAllProduct");
            }
            return View(viewModel);

        }
       
        [HttpPost]
        public IActionResult UpdateProduct(ProductRequestModel product)
        {
            var update = _ProductService.Update(product);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return View(update.Message);
            }
            return RedirectToAction("SuperAdminDashBord", "User");
        }

    }
}
