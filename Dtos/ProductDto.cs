using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Dtos
{
    public class ProductRequestModel
    {
        public string? Name { get; set; }
        public string? Id { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public MultiSelectList Categories { get; set; }
        public string? CategoryId { get; set; }
        public List<IFormFile> ProfilePicture { get; set; }
        public bool IsAvailable { get; set; } = true;



      

    }
    public class ProductResponseModel
    {
        public string? CurrentUser;
        public string? Name { get; set; }
        public string? Id { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Description { get; set; }
        public string? CategoryId { get; set; }
        public CategoryResponseModel Category { get; set; }
        public List<string> Images { get; set; }
        public string CategoryName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCrated { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
        public DateTime? DateDeleted { get; set; }
        public List<Transaction> transactions { get; set; } = new List<Transaction>();



    }
}
