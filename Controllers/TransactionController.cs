using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.implementation;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Enum;
using System.Security.Claims;

namespace RealEstate_Mvc_.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transferService;
        private readonly ICurrentUser _currentUser;

        public TransactionController(ITransactionService transferService, ICurrentUser currentUser)
        {
            _transferService = transferService;
            _currentUser = currentUser;
        }
        [HttpGet]
        public IActionResult EnterPasscode(string productId, string email)
        {
            var model = new TransactionRequestModel
            {
                ProductId = productId,
                Email = email,
                
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult createTransfer(TransactionRequestModel transaction)
        {



            var createTransaction = _transferService.Create(transaction);

            if (createTransaction.Data == null)
            {
                ViewBag.Message = createTransaction.Message;
                if(User.FindFirst(ClaimTypes.Role) != null && User.FindFirst(ClaimTypes.Role).Value == "Customer")
                {
                  return RedirectToAction("GetAllProduct","Property");
                }
                else
                {
                  return RedirectToAction("SuperAdminDashBord","User");
                }

            }
            return View("Receipt",createTransaction.Data);

        }
        [HttpGet]
        public IActionResult Receipt()
        {
            return View();

        }

        [HttpGet]
        public IActionResult DeleteTransfer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteTransfer(string RefNumber)
        {

            var delete = _transferService.Delete(RefNumber);
            if (delete.Data == null)
            {
                ViewBag.Message = delete.Message;
                return RedirectToAction("GetAllTransfer");
            }
            return View(delete);
        }


        [HttpGet]
        public IActionResult GetAllTransfer()
        {
            var getAllTransfer = _transferService.GetAll();
            return View(getAllTransfer);
        }

        [HttpGet]
        public IActionResult GetTransferByPropertyId(string PropertyId)
        {
            var getTransferByPropertyId = _transferService.GetByProductId(PropertyId);
            if (getTransferByPropertyId.Data == null)
            {
                ViewBag.Message = getTransferByPropertyId.Message;
                return RedirectToAction("GetAllTransfer");
            }
            return View(getTransferByPropertyId);

        }
        [HttpGet]
        public IActionResult GetTransferByRefNumber(string RefNumber)
        {
            var getTransferByRefNumber = _transferService.GetByRefNumber(RefNumber);
            if (getTransferByRefNumber.Data == null)
            {
                ViewBag.Message = getTransferByRefNumber.Message;
                return RedirectToAction("GetAllTransfer");
            }
            return View(getTransferByRefNumber);

        }
        [HttpGet]
        public IActionResult GetByCustomerId(string Id)
        {
            var getTransferById = _transferService.GetByCustomerId(Id);
            if (getTransferById.Data == null)
            {
                ViewBag.Message = getTransferById.Message;
                return RedirectToAction("GetAllTransfer");
            }
            return View(getTransferById);

        }
        public IActionResult GetTransferByEmail()
        {
            var email = _currentUser.GetCurrentUser();
            var getTransferByEmail = _transferService.GetByCustomerEmail(email);
            if (getTransferByEmail.Data == null)
            {
                ViewBag.Message = getTransferByEmail.Message;
                return RedirectToAction("GetAllTransfer");
            }
            return View(getTransferByEmail);

        }


        [HttpGet]
        public IActionResult UpdateTransfer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateTransfer(string RefrenceNumber, TransactionRequestModel transactionRequestModel)
        {
            var update = _transferService.Update(RefrenceNumber, transactionRequestModel);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllTransfer");
            }
            return View(update);
        }
    }


  
}
