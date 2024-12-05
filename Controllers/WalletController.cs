using Microsoft.AspNetCore.Mvc;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;

namespace RealEstate_Mvc_.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly ICurrentUser _currentUser;
        public WalletController(IWalletService walletService,ICurrentUser currentUser)
        {
            _walletService = walletService;
            _currentUser = currentUser;
        }
        [HttpGet]
        public IActionResult WalletLandingPage()
        {
            var wallet = _walletService.GetByCustomerEmail(_currentUser.GetCurrentUser());
            return View(wallet.Data);
        }
       
       
        [HttpPost]
        public IActionResult DisableWallet(string WalletId)
        {

            var wallet = _walletService.DisableWallet(WalletId);
            if (wallet.Data == null)
            {
                ViewBag.Message = wallet.Message;
                return RedirectToAction("GetAllWallet");
            }
            return View(wallet);
        }


        [HttpGet]
        public IActionResult GetAllWallet()
        {
            var getAllWallet = _walletService.GetAll();
            return View(getAllWallet);
        }

        [HttpGet]
        public IActionResult GetWalletByEmail(string Email)
        {
            var getById = _walletService.GetByCustomerEmail(Email);
            if (getById.Data == null)
            {
                ViewBag.Message = getById.Message;
                return RedirectToAction("GetAllWallet");
            }
            return View(getById);

        }
        [HttpGet]
        public IActionResult GetWalletByPassKey(string PassKey)
        {
            var getByPassKey = _walletService.GetByPassKey(PassKey);
            if (getByPassKey.Data == null)
            {
                ViewBag.Message = getByPassKey.Message;
                return RedirectToAction("GetAllWallet");
            }
            return View(getByPassKey);

        }
        [HttpGet]
        public IActionResult UpdateWallet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateWallet(WalletRequestModel wallet)
        {
            var update = _walletService.Update(wallet);
            if (update.Data == null)
            {
                ViewBag.Message = update.Message;
                return RedirectToAction("GetAllWallet");
            }
            return View(update);
        }
    }
}
