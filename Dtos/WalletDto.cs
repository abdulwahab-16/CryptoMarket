namespace RealEstate_Mvc_.Dtos
{
    public class WalletRequestModel
    {
        public string? PassCode { get; set; }
    }
    public class WalletResponseModel
    {
        public double Balance { get; set; }
        public string? WalletAddress { get; set; }
    }
}
