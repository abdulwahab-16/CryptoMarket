using Azure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class TransactionService : ITransactionService
    {
        //MakePayment is create transaction
        //Add money to wallet
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _propertyRepository;
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductService _propertyService;
        private readonly IWalletRepository _walletService;

        public TransactionService( IProductService propertyService, ITransactionRepository transactionRepository, IUserRepository userRepository, IProductRepository propertyRepository, ICurrentUser currentUser, ICustomerRepository customerRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _propertyRepository = propertyRepository;
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _propertyService = propertyService;
            _walletService = walletRepository;
        }
        public BaseResponse<TransactionResponseModel> Create(TransactionRequestModel transaction)
        {
            var product = _propertyRepository.GetById(transaction.ProductId);
            var customer = _customerRepository.GetByEmail(_currentUser.GetCurrentUser());
            var wallet = _walletService.GetByCustomerEmail(_currentUser.GetCurrentUser());
            if (product == null || customer == null)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Message = "Customer Email or PropertyId not found",
                    Success = false,
                    Data = null,
                };
            }
            if (customer.Wallet.Balance < product.Price)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Message = "You dont have enough money in your wallet",
                    Success = false,
                    Data = null,
                };
            }
            if (product.IsAvailable == false)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Message = "product is not available",
                    Success = false,
                    Data = null,
                };
            }
         
            Transaction transaction1 = new Transaction
            {
                RefNumber = $"tranc{Guid.NewGuid().ToString()}wer1232",
                CreatedBy = _currentUser.GetCurrentUser(),
                CustomerId = customer.Id,
                Email = transaction.Email,
                Price = product.Price,
                Customer = customer,
                IsSucessfull = true,
                DateCrated = DateTime.Now,
                Products = product,
                ProductsId = product.Id,    
            };
            customer.Wallet.Balance -= product.Price;
            product.IsAvailable = false;
            _propertyRepository.Update(product);
            _customerRepository.Update(customer);
            _transactionRepository.Create(transaction1);

            return new BaseResponse<TransactionResponseModel>
            {
                Message = "Transaction Created Succesfully",
                Success = true,
                Data = new TransactionResponseModel
                {

                    RefNumber = transaction1.RefNumber,
                    Email = transaction1.Email,
                    Price = transaction1.Price,
                    IsSucessfull = transaction1.IsSucessfull,
                    ProductId = product.Id, 
                }

            };
        }

        public BaseResponse<TransactionResponseModel> Delete(string RefNumber)
        {
            var transaction = _transactionRepository.GetByRefNumber(RefNumber);

            if (transaction == null)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Message = "transaction RefNumber not found",
                    Success = false,
                    Data = null,
                };
            }


            transaction.IsDeleted = true;
            transaction.DeletedBy = _currentUser.GetCurrentUser();
            transaction.DateDeleted = DateTime.UtcNow;
            _transactionRepository.Update(transaction);

            return new BaseResponse<TransactionResponseModel>
            {
                Message = "Deleted ",
                Success = true,
                Data = new TransactionResponseModel
                {

                    RefNumber = transaction.RefNumber,
                    Email = transaction.Email,
                    Price = transaction.Price,
                    ProductId = transaction.ProductsId,
                }
            };


        }

        public BaseResponse<List<TransactionResponseModel>> GetAll()
        {
            var transactions = _transactionRepository.GetAll();
            if (transactions == null)
            {
                return new BaseResponse<List<TransactionResponseModel>>
                {
                    Message = "no transaction found",
                    Success = false,
                    Data = null,
                };
            }
            var listOfTransactions = transactions.Select(x => new TransactionResponseModel
            {
                RefNumber = x.RefNumber,
                Email = x.Email,
                Price = x.Price,
                ProductId = x.ProductsId,   
                IsSucessfull = x.IsSucessfull,

            }).ToList();
            return new BaseResponse<List<TransactionResponseModel>>
            {
                Message = "All transaction",
                Success = true,
                Data = listOfTransactions
            };
        }

        public BaseResponse<List<TransactionResponseModel>> GetByCustomerId(string Id)
        {
            var transfer = _transactionRepository.GetByCustomerId(Id);
            if (transfer == null)
            {
                return new BaseResponse<List<TransactionResponseModel>>
                {
                    Message = "transactions not found",
                    Success = false,
                    Data = null
                };
            }
            var listOfTransaction = transfer.Select(x => new TransactionResponseModel
            {
                RefNumber = x.RefNumber,
                Email = x.Email,
                Price = x.Price,
                ProductId = x.ProductsId,
                IsSucessfull = x.IsSucessfull,
            }).ToList();
            return new BaseResponse<List<TransactionResponseModel>>
            {
                Message = "All transactions",
                Success = true,
                Data = listOfTransaction
            };
        }

        public BaseResponse<List<TransactionResponseModel>> GetByCustomerEmail(string Email)
        {
            var user = _userRepository.Get(Email);
            var transfer = _transactionRepository.GetByCustomerEmail(Email);
            if (transfer == null || user == null)
            {
                return new BaseResponse<List<TransactionResponseModel>>
                {
                    Message = "transactions or userEmail not found",
                    Success = false,
                    Data = null
                };
            }
            var listOfTransaction = transfer.Select(x => new TransactionResponseModel
            {
                RefNumber = x.RefNumber,
                Email = x.Email,
                Price = x.Price,
                ProductId = x.ProductsId,
                IsSucessfull = x.IsSucessfull,
            }).ToList();
            return new BaseResponse<List<TransactionResponseModel>>
            {
                Message = "All transfer",
                Success = true,
                Data = listOfTransaction
            };
        }

        public BaseResponse<List<TransactionResponseModel>> GetByProductId(string ProductId)
        {
            var proprty = _propertyRepository.GetById(ProductId);
            var exist = _transactionRepository.GetByProductId(ProductId);
            if (exist == null || proprty == null)
            {
                return new BaseResponse<List<TransactionResponseModel>>
                {
                    Message = "property or transaction Id not found",
                    Success = false,
                    Data = null,
                };
            }
            var listOfTransaction = exist.Select(x => new TransactionResponseModel
            {
                RefNumber = x.RefNumber,
                Email = x.Email,
                Price = x.Price,
                ProductId = x.ProductsId,
                IsSucessfull = x.IsSucessfull,
            }).ToList();
            return new BaseResponse<List<TransactionResponseModel>>
            {
                Message = "All transfer on productId",
                Success = true,
                Data = listOfTransaction
            };

        }


        public BaseResponse<TransactionResponseModel> GetByRefNumber(string RefNumber)
        {
            var transaction = _transactionRepository.GetByRefNumber(RefNumber);

            if (transaction == null)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Message = "transaction not found",
                    Success = false,
                    Data = null,
                };
            }

            return new BaseResponse<TransactionResponseModel>
            {
                Message = "Gotten the transaction",
                Success = true,
                Data = new TransactionResponseModel
                {
                    RefNumber = transaction.RefNumber,
                    Email = transaction.Email,
                    Price = transaction.Price,
                    ProductId = transaction.ProductsId, 
                    IsSucessfull = transaction.IsSucessfull,
                }
            };




        }

        public BaseResponse<TransactionResponseModel> Update(string RefrenceNumber, TransactionRequestModel transactionRequestModel)
        {
            var transaction = _transactionRepository.GetByRefNumber(RefrenceNumber);
            var product = _propertyRepository.GetById(transactionRequestModel.ProductId);

            if (transaction == null)
            {
                return new BaseResponse<TransactionResponseModel>
                {
                    Message = "transaction not found",
                    Success = false,
                    Data = null,
                };
            }

            transaction.ProductsId = transactionRequestModel.ProductId;
            transaction.Price = product.Price;
            transaction.CustomerId = transaction.CustomerId;
            transaction.Email = transactionRequestModel.Email;
            transaction.CreatedBy = _currentUser.GetCurrentUser();

            var updatedTransaction = _transactionRepository.Update(transaction);



            return new BaseResponse<TransactionResponseModel>
            {
                Message = "Updated ",
                Success = true,
                Data = new TransactionResponseModel
                {
                    RefNumber = transaction.RefNumber,
                    Email = transaction.Email,
                    Price = transaction.Price,
                    ProductId = transaction.ProductsId, 
                    IsSucessfull = transaction.IsSucessfull,
                }
            };

        }
    }

}