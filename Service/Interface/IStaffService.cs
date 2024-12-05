using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Models.Entities;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface IStaffService
    {
        BaseResponse<StaffResponseModel> Create(StaffRequestModel Staff);
        BaseResponse<StaffResponseModel> GetByTagNumber(string StaffNumber);
        BaseResponse<StaffResponseModel> GetByEmail(string Email);
        BaseResponse<StaffResponseModel> GetById(string Id);
        BaseResponse<List<StaffResponseModel>> GetAll();
        BaseResponse<StaffResponseModel> Update(StaffRequestModel StaffRequestModel);
        BaseResponse<StaffResponseModel> Delete(string StaffNumber);
    }
}
