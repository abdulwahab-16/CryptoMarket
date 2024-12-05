using RealEstate_Mvc_.Dtos;

namespace RealEstate_Mvc_.Service.Interface
{
    public interface ITransactionService
    {
        BaseResponse<TransactionResponseModel> Create(TransactionRequestModel transaction);
        BaseResponse<TransactionResponseModel> GetByRefNumber(string RefNumber);
        BaseResponse<List<TransactionResponseModel>> GetByCustomerEmail(string CustomerEmail);
        BaseResponse<List<TransactionResponseModel>> GetByCustomerId(string Id);
        BaseResponse<List<TransactionResponseModel>> GetByProductId(string ProductId);
        BaseResponse<List<TransactionResponseModel>> GetAll();
        BaseResponse<TransactionResponseModel> Update(string RefrenceNumber, TransactionRequestModel transactionRequestModel);
        BaseResponse<TransactionResponseModel> Delete(string RefNumber);
    }
}