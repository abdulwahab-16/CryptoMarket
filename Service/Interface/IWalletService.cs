using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Models.Entities;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface IWalletService
    {
        BaseResponse<WalletResponseModel> GetByCustomerEmail(string Email);
        BaseResponse<WalletResponseModel> GetByPassKey(string Id);
         BaseResponse<int> GetAll();
        BaseResponse<WalletResponseModel> Update(WalletRequestModel wallet);
        BaseResponse<WalletResponseModel> DisableWallet(string WalletId);
    }
}
