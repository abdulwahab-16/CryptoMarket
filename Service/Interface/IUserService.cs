using RealEstate_Mvc_.Dtos;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface IUserService
    {

        BaseResponse<UserResponseModel> Create(UserRequestModel user);
        BaseResponse<UserResponseModel> Get(string email);
        BaseResponse<UserResponseModel> Login(UserLoginRequestModel userLoginRequestModel);
        BaseResponse<List<UserResponseModel>> GetByRoleId(string Id);
        BaseResponse<List<UserResponseModel>> GetAll();
        BaseResponse<UserResponseModel> Update(UserRequestModel userRequestModel);
        BaseResponse<UserResponseModel> Delete(string Email);
    }
}