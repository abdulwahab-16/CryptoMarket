using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Dtos
{
    public class TransactionRequestModel
    {

        public string Email { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public string PassCode { get; set; } = default!;

    }
    public class TransactionResponseModel
    {
        public string RefNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public double Price { get; set; }
        public string ProductId { get; set; } = default!;
        public bool IsSucessfull { get; set; }

    }
}
