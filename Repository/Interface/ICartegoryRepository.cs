using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Repository.Interface
{
    public interface ICartegoryRepository
    {
        bool Check(string Name);
        Category Create(Category cartegory);
        Category GetByName(string Name);
        List<Category> GetAll();
        Category Update(Category cartegory);
        Category GetById(string Id);

    }
}