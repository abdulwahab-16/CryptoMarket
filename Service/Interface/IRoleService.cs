using RealEstate_Mvc_.Dtos;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface IRoleService
    {
        BaseResponse<RoleResponseModel> Create(RoleRequestModel role);
        BaseResponse<RoleResponseModel> GetByName(string Name);
        BaseResponse<List<RoleResponseModel>> GetAll();
        BaseResponse<RoleResponseModel> Update(RoleRequestModel role);
        BaseResponse<RoleResponseModel> Delete(string Name);
    }
}