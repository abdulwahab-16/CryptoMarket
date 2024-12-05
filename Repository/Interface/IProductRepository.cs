using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface IProductRepository
    {
        Products Create(Products product);
        Products GetById(string Id);
        List<Products> GetByPrice(double Price);
        List<Products> GetByDescription(string Description);
        List<Products> GetByCartegoryId(string CartegoryId);
        List<Products> GetByAvailability();
        List<Products> GetAll();
        Products Update(Products product);

    }
}