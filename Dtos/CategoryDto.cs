using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Dtos
{
    public class CategoryRequestModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Email { get; set; } = default!;



    }
    public class CategoryResponseModel
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<ProductResponseModel> products { get; set; } = new List<ProductResponseModel>();

    }
}
