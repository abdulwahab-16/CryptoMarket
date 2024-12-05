using Microsoft.AspNetCore.Mvc;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Service.Interface;

namespace RealEstate_Mvc_.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICartegoryService _cartegoryService;
        public CategoryController(ICartegoryService cartegoryService)
        {
            _cartegoryService = cartegoryService;
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryRequestModel cartegoryRequestModel)
        {
            var createCartegory = _cartegoryService.Create(cartegoryRequestModel);
            if (createCartegory.Data == null)
            {
                ViewBag.Message = createCartegory.Message;
                return View();

            }
            ViewBag.Message = createCartegory.Message;
            return RedirectToAction("SuperAdminDashBord", "User");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string Name)
        {

            var delete = _cartegoryService.Delete(Name);
            if (delete.Data == null)
            {

                ViewBag.Message = delete.Message;
                return RedirectToAction("GetAllCartegory");
            }
            return RedirectToAction("GetAllCartegory");
        }
        [HttpGet]
        public IActionResult GetAllCartegory()
        {
            var getAllCartegory = _cartegoryService.GetAll();
            return View(getAllCartegory.Data);

        }
       
      
        [HttpGet]
        public IActionResult GetByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                ViewBag.Message = "Please enter a category name.";
                return View();
            }

            var response = _cartegoryService.GetByName(Name);
            if (response.Data == null)
            {
                ViewBag.Message = "Category not found.";
                return View();
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult GetById(string Id)
        {

            if (string.IsNullOrEmpty(Id))
            {
                ViewBag.Message = "Please enter a category Id.";
                return View();
            }
            var getById = _cartegoryService.GetById(Id);
            if (getById.Data == null)
            {
                ViewBag.Message = getById.Message;
                return RedirectToAction("GetAllCartegory");
            }
            return View(getById.Data);


        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(CategoryRequestModel cartegory)
        {
            var update = _cartegoryService.Update(cartegory);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllCartegory");
            }
            return RedirectToAction("GetAllCartegory");

        }
    }
}
