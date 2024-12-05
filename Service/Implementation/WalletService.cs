using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRoleRepository _roleRepository;
        private readonly ContextClass _contextClass;
        private readonly IWalletRepository _walletRepository;
        public WalletService(IWalletRepository walletRepository,IUserRepository userRepository, ICurrentUser currentUser, IRoleRepository roleRepository, ContextClass contextClass)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _roleRepository = roleRepository;
            _contextClass = contextClass;
            _walletRepository = walletRepository;
        }

        public BaseResponse<WalletResponseModel> DisableWallet(string WalletId)
        {
            var wallet = _walletRepository.GetById(WalletId);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "wallet Not found",
                    Success = false,
                    Data = null,
                };
            }
            wallet.IsDisabled = true;
            _walletRepository.Update(wallet);

            return new BaseResponse<WalletResponseModel>
            {
                Message = "Disabled",
                Success = true,
                Data = new WalletResponseModel
                {
                    WalletAddress = wallet.WalletAddress,
                    Balance = wallet.Balance,
                }
            };
        }

        public BaseResponse<int> GetAll()
        {
            var wallet = _walletRepository.GetAll();
            if (wallet == null)
            {
                return new BaseResponse<int>
                {
                    Message = " no wallet available",
                    Success = false,
                    Data = 0,
                };
            }
            int count = 0;
            foreach (var item in wallet)
            {
                count++;    
            }
            return new BaseResponse<int>
            {
                Message = "All wallets",
                Success = true,
                Data = count
            };
        }

        public BaseResponse<WalletResponseModel> GetByCustomerEmail(string Email)
        {
            var wallet = _walletRepository.GetByCustomerEmail(Email);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "wallet not found",
                    Success = false,
                    Data = null,
                };
            }
            return new BaseResponse<WalletResponseModel>
            {
                Message = "Gotten the wallet",
                Success = true,
                Data = new WalletResponseModel
                {
                   Balance = wallet.Balance,
                   WalletAddress = wallet.WalletAddress ,
                }
            };
        }

        public BaseResponse<WalletResponseModel> GetByPassKey(string Id)
        {
            var wallet = _walletRepository.GetByPassKey(Id);
            if (wallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "wallet not found",
                    Success = false,
                    Data = null,
                };
            }
            return new BaseResponse<WalletResponseModel>
            {
                Message = "Gotten the wallet",
                Success = true,
                Data = new WalletResponseModel
                {
                    Balance = wallet.Balance,
                    WalletAddress = wallet.WalletAddress,
                }
            };
        }

        public BaseResponse<WalletResponseModel> Update(WalletRequestModel wallet)
        {
            var getWallet = _walletRepository.GetByPassKey(wallet.PassCode);
            if (getWallet == null)
            {
                return new BaseResponse<WalletResponseModel>
                {
                    Message = "wallet not found",
                    Success = false,
                    Data = null,
                };
            }
            getWallet.PassCode = wallet.PassCode;
            _walletRepository.Update(getWallet);



            return new BaseResponse<WalletResponseModel>
            {
                Message = "Updated the wallet",
                Success = true,
                Data = new WalletResponseModel
                {
                 Balance = getWallet.Balance,
                 WalletAddress = getWallet.WalletAddress,   
                }
            };
        }
    }
}
