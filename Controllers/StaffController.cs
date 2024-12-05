using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Service.Interface;

namespace RealEstate_Mvc_.Controllers
{
    public class StaffController : Controller
    {

        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpGet]
        public IActionResult createStaff()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createStaff(StaffRequestModel Staff)
        {
            var createStaff = _staffService.Create(Staff);
            if (createStaff.Data == null)
            {
                ViewBag.Message = createStaff.Message;
                return View();

            }
            return RedirectToAction("SuperAdminDashBord", "User");

        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(string StaffNumber)
        {

            var delete = _staffService.Delete(StaffNumber);
            if (delete.Data == null)
            {
                ViewBag.Message = delete.Message;
                return RedirectToAction("GetAllStaff");
            }
            return View(delete);
        }
        [HttpGet]
        public IActionResult GetAllStaff()
        {
            var getAllStaff = _staffService.GetAll();
            return View(getAllStaff);
        }
        [HttpGet]
        public IActionResult GetByEmail(string Email)
        {
            var getByEmail = _staffService.GetByEmail(Email);
            if (getByEmail.Data == null)
            {
                ViewBag.Message = getByEmail.Message;
                return RedirectToAction("GetAllStaff");
            }
            return View(getByEmail);

        }
        [HttpGet]
        public IActionResult GetById(string Id)
        {
            var getById = _staffService.GetById(Id);
            if (getById.Data == null)
            {
                ViewBag.Message = getById.Message;
                return RedirectToAction("GetAllStaff");
            }
            return View(getById);

        }
        [HttpGet]
        public IActionResult GetByStaffNumber(string StaffNumber)
        {
            var getByStaffNumber = _staffService.GetByTagNumber(StaffNumber);
            if (getByStaffNumber.Data == null)
            {
                ViewBag.Message = getByStaffNumber.Message;
                return RedirectToAction("GetAllStaff");
            }
            return View(getByStaffNumber);

        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(StaffRequestModel Staff)
        {
            var update = _staffService.Update(Staff);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllStaff");
            }
            return View(update);
        }
    }
}
