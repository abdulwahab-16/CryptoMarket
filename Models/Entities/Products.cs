using RealEstate_Mvc_.Models.Entities;
using RealEstateMvc.Models.Enum;

namespace RealEstateMvc.Models.Entities
{
    public class Products
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double Price { get; set; } 
        public string? Name { get; set; }    
        public bool IsAvailable { get; set; } = true;
        public string? Description { get; set; } 
        public string? CreatedBy { get; set; }
        public DateTime DateCrated { get; set; } = DateTime.Now;
        public string? DeletedBy { get; set; }
        public DateTime? DateDeleted { get; set; } 
        public bool IsDeleted { get; set; }
       public List<Transaction> transactions { get; set; } = new List<Transaction>();
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<string> Images { get; set; }=new List<string>();
        public string? CategoryName { get; set; }




    }
}