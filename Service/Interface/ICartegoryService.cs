using RealEstate_Mvc_.Dtos;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface ICartegoryService
    {
        BaseResponse<CategoryResponseModel> Create(CategoryRequestModel cartegory);
        BaseResponse<CategoryResponseModel> GetByName(string Name);
        BaseResponse<CategoryResponseModel> GetById(string Id);
        BaseResponse<List<CategoryResponseModel>> GetAll();
        BaseResponse<CategoryResponseModel> Update(CategoryRequestModel cartegory);
        BaseResponse<CategoryResponseModel> Delete(string Name);




    }
}