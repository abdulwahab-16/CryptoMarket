using RealEstate_Mvc_.Dtos;
using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface IProductService
    {
        BaseResponse<ProductResponseModel> Create(ProductRequestModel product);
        BaseResponse<ProductResponseModel> GetById(string Id);
        BaseResponse<List<ProductResponseModel>> GetByPrice(double Price);
        BaseResponse<List<ProductResponseModel>> GetByDescription(string Description);
        BaseResponse<List<ProductResponseModel>> GetByCartegoryId(string CartegoryId);
        BaseResponse<List<ProductResponseModel>> GetByAvailability();
        BaseResponse<List<ProductResponseModel>> GetAll();
        BaseResponse<ProductResponseModel> Update(ProductRequestModel product);
        BaseResponse<ProductResponseModel> Delete(string Id);
    }
}