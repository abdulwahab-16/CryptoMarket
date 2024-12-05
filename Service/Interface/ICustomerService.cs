using RealEstate_Mvc_.Dtos;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface ICustomerService
    {
        BaseResponse<CustomerResponseModel> Create(CustomerRequestModel customer);
        BaseResponse<CustomerResponseModel> GetByEmail(string Email);
        BaseResponse<CustomerResponseModel> AddMoneyToWallet( double amount,string Passcode);
        BaseResponse<CustomerResponseModel> GetById(string Id);
        BaseResponse<List<CustomerResponseModel>> GetAll();
        BaseResponse<CustomerResponseModel> Update(CustomerRequestModel customer);
        BaseResponse<CustomerResponseModel> Delete(string Email);
    }
}