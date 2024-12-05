using RealEstate_Mvc_.Dtos;

namespace RealEstate_Mvc_.ViewModel
{
    public class ProductViewModel
    {
        public List<ProductResponseModel> product { get; set; }
        public List<CategoryResponseModel> Categories { get; set; }
    }
}
