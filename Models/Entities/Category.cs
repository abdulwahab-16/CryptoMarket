using RealEstate_Mvc_.Models.Entities;

namespace RealEstateMvc.Models.Entities
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime DateCrated { get; set; } = DateTime.Now;
       public List<Products> Products { get; set; } = new List<Products>();
        public string? DeletedBy { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }

    }
}