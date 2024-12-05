using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Models.Entities;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;
using System.Xml.Linq;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IWalletRepository _walletRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository, ICurrentUser currentUser, IRoleRepository roleRepository, IWebHostEnvironment webHostEnvironment, IWalletRepository walletRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
            _roleRepository = roleRepository;
            _webHostEnvironment = webHostEnvironment;
            _walletRepository = walletRepository;
        }

        public BaseResponse<CustomerResponseModel> AddMoneyToWallet(double amount, string Passcode)
        {
            var customer = _customerRepository.GetByEmail(_currentUser.GetCurrentUser());
            var wallet = _walletRepository.GetByCustomerEmail(_currentUser.GetCurrentUser());
            if (customer.Wallet.PassCode != Passcode)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer Passcode not correct",
                    Success = false,
                    Data = null,
                };
            }
            if (customer == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer Email already Exist",
                    Success = false,
                    Data = null,
                };
            }

            customer.Wallet.Balance += amount;
            _customerRepository.Update(customer);
            return new BaseResponse<CustomerResponseModel>
            {
                Message = "Wallet Updated Succesfully",
                Success = true,
                Data = new CustomerResponseModel
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Age = customer.Age,
                    Address = customer.Address,
                    Gender = customer.Gender,
                }
            };

        }


        public BaseResponse<CustomerResponseModel> Create(CustomerRequestModel customer)
        {

            var customerExist = _customerRepository.GetByEmail(customer.Email);
            var userExist = _userRepository.Check(customer.Email);
            string uniqueFileName = null;
            if (customer.ProfilePicture != null)
            {
                 uniqueFileName = Guid.NewGuid().ToString() + "_" + customer.ProfilePicture.FileName;
                if (customer.ProfilePicture.FileName != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "properties");
                    if (Path.Exists(uploadsFolder))
                    {
                        var folder = Directory.CreateDirectory(uploadsFolder);
                    }
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        customer.ProfilePicture.CopyTo(fileStream);
                    }
                }

            }
            if (customerExist != null || userExist)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer Email already Exist",
                    Success = false,
                    Data = null,
                };
            }
            if (customer.Password != customer.ConfirmPassword)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "password does not correspond",
                    Success = false,
                    Data = null,
                };
            }
            var newPasswordd = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            DateTime currentDatee = DateTime.Now;
            DateTime dobt = currentDatee.AddYears(-customer.Age);
            Wallet wallet = new Wallet
            {
                Id = Guid.NewGuid().ToString(),
                Balance = 0,
                CustomerEmail = customer.Email,
                IsDisabled = false,
                PassCode = customer.PassCode,
                WalletAddress = $"0x3456{Guid.NewGuid().ToString()}",

            };
            _walletRepository.Create(wallet);
            Customer customers = new Customer
            {
                Email = customer.Email,
                TagNumber = $"cust{Guid.NewGuid().ToString()}",
                IsDeleted = false,
                PhoneNumber = customer.PhoneNumber,
                LastName = customer.LastName,
                Address = customer.Address,
                Age = customer.Age,
                CreatedBy = customer.Email,
                Dob = dobt,
                FirstName = customer.FirstName,
                Gender = customer.Gender,
                ProfilePicturePath = uniqueFileName,
                WallId = wallet.Id,
                Wallet= wallet, 
                

            };
            User Users = new User
            {
                Email = customer.Email,
                Password = newPasswordd,
                CreatedBy = customer.Email,
                UserRoles = new List<UserRole>(),
                FullName = customer.FirstName + " " + customer.LastName
            };
          

            var roles = _roleRepository.GetByName("Customer");
            if (roles == null)
            {

                Role newRole = new Role
                {
                    Name = "Customer",
                    Description = "Customer in the application",
                    IsDeleted = false,
                    CreatedBy = customer.Email

                };
                Users.UserRoles.Add(new UserRole
                {
                    User = Users,
                    RoleId = newRole.Id,
                    Role = newRole,
                    UserId = Users.Id


                });

            }
            else
            {
                Users.UserRoles.Add(new UserRole
                {
                    User = Users,
                    RoleId = roles.Id,
                    Role = roles,
                    UserId = Users.Id


                });
            }
           
            _customerRepository.Create(customers);
            _userRepository.Create(Users);
          
            return new BaseResponse<CustomerResponseModel>
            {
                Message = "Customer Created Succesfully",
                Success = true,
                Data = new CustomerResponseModel
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Age = customer.Age,
                    Address = customer.Address,
                    Gender = customer.Gender,
                }
            };
        }
    
      

        public BaseResponse<CustomerResponseModel> Delete(string Email)
        {
            var customer = _customerRepository.GetByEmail(Email);
            var walletExist = _walletRepository.GetByCustomerEmail(Email);
            var user = _userRepository.Get(Email);
            if (customer == null || user == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer Email not found",
                    Success = false,
                    Data = null,
                };
            }
            customer.IsDeleted = true;

            customer.DateCrated = DateTime.Now;
            customer.DeletedBy = _currentUser.GetCurrentUser();
            user.IsDeleted = true;
            user.DateCrated = DateTime.Now;
            walletExist.IsDisabled= true;
            user.DeletedBy = _currentUser.GetCurrentUser();
            _walletRepository.Update(walletExist);
            var newCustomer = _customerRepository.Update(customer);
            _userRepository.Update(user);



            return new BaseResponse<CustomerResponseModel>
            {
                Message = "Deleted ",
                Success = true,
                Data = new CustomerResponseModel
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Age = customer.Age,
                    Address = customer.Address,
                    Gender = customer.Gender,
                }
            };
        }

        public BaseResponse<List<CustomerResponseModel>> GetAll()
        {
            var customers = _customerRepository.GetAll();
            if (customers == null)
            {
                return new BaseResponse<List<CustomerResponseModel>>
                {
                    Message = "customers not found",
                    Success = true,
                    Data = null
                };
            }

            var listOfCustomers = customers.Select(x => new CustomerResponseModel
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Age = x.Age,
                Address = x.Address,
                Gender = x.Gender,
                ProfilePicture = x.ProfilePicturePath,
                Wallet = x.Wallet.Balance,
                Transactions = x.Transactions.Select(c => new TransactionResponseModel
                {
                    Email = c.Email,
                    IsSucessfull = c.IsSucessfull,
                    Price = c.Price,
                    RefNumber = c.RefNumber,
                }).ToList()



            }).ToList();

            return new BaseResponse<List<CustomerResponseModel>>
            {
                Message = "All customers",
                Success = true,
                Data = listOfCustomers
            };
        }

        public BaseResponse<CustomerResponseModel> GetByEmail(string Email)
        {
            var customerExist = _customerRepository.GetByEmail(Email);
            var userExist = _userRepository.Check(Email);
            var wallet = _walletRepository.GetByCustomerEmail(Email);

            if (customerExist == null || !userExist)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer Email Not found",
                    Success = false,
                    Data = null,
                };
            }
            var customer = _customerRepository.GetByEmail(Email);
            var user = _userRepository.Get(Email);
            return new BaseResponse<CustomerResponseModel>
            {
                Message = "Gotten the customer",
                Success = true,
                Data = new CustomerResponseModel
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Age = customer.Age,
                    Address = customer.Address,
                    Gender = customer.Gender,
                    Wallet = wallet.Balance,
                    ProfilePicture = customer.ProfilePicturePath,
                    Transactions = customer.Transactions.Select(c => new TransactionResponseModel
                    {
                        Email = c.Email,
                        IsSucessfull = c.IsSucessfull,
                        Price = c.Price,
                        RefNumber = c.RefNumber,
                    }).ToList()
                }
            };
        }

        public BaseResponse<CustomerResponseModel> GetById(string Id)
        {

            var customer = _customerRepository.GetById(Id);
            var user = _userRepository.Get(Id);

            if (customer == null || user == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer  not found",
                    Success = false,
                    Data = null,
                };
            }
            return new BaseResponse<CustomerResponseModel>
            {
                Message = "Gotten the customer",
                Success = true,
                Data = new CustomerResponseModel
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Age = customer.Age,
                    Address = customer.Address,
                    Gender = customer.Gender,
                    Wallet = customer.Wallet.Balance,
                    ProfilePicture = customer.ProfilePicturePath,
                    Transactions = customer.Transactions.Select(c => new TransactionResponseModel
                    {
                        Email = c.Email,
                        IsSucessfull = c.IsSucessfull,
                        Price = c.Price,
                        RefNumber = c.RefNumber,
                    }).ToList()
                }
            };
        }

        public BaseResponse<CustomerResponseModel> Update(CustomerRequestModel customer)
        {
            var custemail = _currentUser.GetCurrentUser();
            var customerExist = _customerRepository.GetByEmail(custemail);
            var userExist = _userRepository.Get(custemail);
            var walletExist = _walletRepository.GetByCustomerEmail(custemail);


            var uniqueFileName = Guid.NewGuid().ToString() + "_" + customer.ProfilePicture.FileName;

            if (customer.ProfilePicture != null)
            {
                if (customer.ProfilePicture.FileName != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "properties");
                    if (Path.Exists(uploadsFolder))
                    {
                        var folder = Directory.CreateDirectory(uploadsFolder);
                    }
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        customer.ProfilePicture.CopyTo(fileStream);
                    }
                }
            }
            if (customerExist == null || userExist == null)
            {
                
                return new BaseResponse<CustomerResponseModel>
                {
                    Message = "Customer Email not found",
                    Success = false,
                    Data = null,
                };
            }
            var newPassword = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            customerExist.Email = customer.Email;
            customerExist.FirstName = customer.FirstName;
            customerExist.LastName = customer.LastName;
            customerExist.PhoneNumber = customer.PhoneNumber;
            customerExist.Age = customer.Age;
            customerExist.Address = customer.Address;
            customerExist.Gender = customer.Gender;
            customerExist.ProfilePicturePath = uniqueFileName;
            customerExist.Dob = customer.Dob;
            userExist.Email = customer.Email;
            userExist.Password = newPassword;
            userExist.CreatedBy = _currentUser.GetCurrentUser();
            customerExist.CreatedBy = _currentUser.GetCurrentUser();
            walletExist.PassCode = customer.PassCode;

            userExist.FullName = customer.FirstName + customer.LastName;
            var customer1 = _customerRepository.Update(customerExist);
            var user1 = _userRepository.Update(userExist);
            _walletRepository.Update(walletExist);



            return new BaseResponse<CustomerResponseModel>
            {

                Message = "Updated the customer",
                Success = true,
                Data = new CustomerResponseModel
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Age = customer.Age,
                    Address = customer.Address,
                    Gender = customer.Gender,
                    Wallet = customerExist.Wallet.Balance,
                    ProfilePicture = customerExist.ProfilePicturePath,
                    Transactions = customerExist.Transactions.Select(c => new TransactionResponseModel
                    {
                        Email = c.Email,
                        IsSucessfull = c.IsSucessfull,
                        Price = c.Price,
                        RefNumber = c.RefNumber,
                    }).ToList()
                }
            };
        }
    }
}