using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Models.Entities
{
    public class Wallet
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? PassCode { get; set; }
        public string? SeedPhrase { get; set; }
        public double  Balance { get; set; }   
        public string? WalletAddress { get; set; }  
        public string  ?CustomerEmail { get; set; }
        public bool ? IsDisabled { get; set; }  
    }
}
