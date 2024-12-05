using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Service.Interface;

namespace RealEstate_Mvc_.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public IActionResult createRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createRole(RoleRequestModel role)
        {
            var createRole = _roleService.Create(role);
            if (createRole.Data == null)
            {
                ViewBag.Message = createRole.Message;
                return View();

            }
            return View();

        }
        [HttpGet]
        public IActionResult DeleteRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteRole(string Name)
        {

            var delete = _roleService.Delete(Name);
            if (delete.Data == null)
            {
                ViewBag.Message = delete.Message;
                return RedirectToAction("GetAllrole");
            }
            return View(delete);
        }


        [HttpGet]
        public IActionResult GetAllRole()
        {
            var getAllRole = _roleService.GetAll();
            return View(getAllRole);
        }

        [HttpGet]
        public IActionResult GetRoleByName(string Name)
        {
            var getByName = _roleService.GetByName(Name);
            if (getByName.Data == null)
            {
                ViewBag.Message = getByName.Message;
                return RedirectToAction("GetAllRole");
            }
            return View(getByName);

        }
        [HttpGet]
        public IActionResult UpdateRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateRole(RoleRequestModel role)
        {
            var update = _roleService.Update(role);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllRole");
            }
            return View(update);
        }
    }
}
