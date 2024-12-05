using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Models.Entities;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;
using System.Security.Claims;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartegoryRepository _cartegoryRepository;
        private readonly ICartegoryService _cartegoryService;
        private readonly ICurrentUser _currentUser;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _StaffRepository;
        public ProductService(ICartegoryService cartegoryService,IProductRepository productRepository, ICartegoryRepository cartegoryRepository, ICurrentUser currentUser, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository, IStaffRepository StaffRepository)
        {
            _cartegoryService = cartegoryService ;
            _productRepository = productRepository;
            _cartegoryRepository = cartegoryRepository;
            _currentUser = currentUser;
            _webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
            _StaffRepository = StaffRepository;
        }
        public BaseResponse<ProductResponseModel> Create(ProductRequestModel product)
        {
            var category = _cartegoryService.GetAll();
            List<string> newList = new List<string>();
            var categoryId = _cartegoryRepository.GetById(product.CategoryId);
            if (product.ProfilePicture != null)
            {
               
                foreach (var item in product.ProfilePicture)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "properties");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyToAsync(fileStream);
                    }
                    newList.Add(uniqueFileName);
                }
            }

            Products products = new Products
            {
                Price = product.Price,
                IsAvailable = true,
                Description = product.Description,
                IsDeleted = false,
                CategoryId = product.CategoryId,
                CreatedBy = _currentUser.GetCurrentUser(),
                DateCrated = DateTime.Now,
                CategoryName = categoryId.Id,
                Images = newList,
                Name = product.Name,  
                Category = categoryId,
                
                



            };
            _productRepository.Create(products);
            return new BaseResponse<ProductResponseModel>
            {
                Message = "product Created Succesfully",
                Success = true,
                Data = new ProductResponseModel
                {
                    Price = product.Price,
                    IsAvailable = product.IsAvailable,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Id = product.Id,
                    Images = newList,
                }
            };
                
            

        }


        public BaseResponse<ProductResponseModel> Update(ProductRequestModel product)
        {

            var category = _cartegoryService.GetAll();
            var exist = _productRepository.GetById(product.Id);
            var get = _cartegoryService.GetById(product.CategoryId);
            var categories = _cartegoryRepository.GetById(exist.CategoryId);
            List<string> newList = null;
            if (product.ProfilePicture != null)
            {
                newList = new List<string>();
                foreach (var item in product.ProfilePicture)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "properties");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyToAsync(fileStream);
                    }
                    newList.Add(uniqueFileName);
                }

            }
            if (exist == null)
            {
                return new BaseResponse<ProductResponseModel>
                {
                    Message = "product Id not found",
                    Success = false,
                    Data = null,
                };
            }
            exist.CategoryId = product.CategoryId;
            exist.Description = product.Description;
            exist.CreatedBy = _currentUser.GetCurrentUser();
            exist.Price = product.Price;
            exist.Images = newList;
            exist.Category = categories;
            exist.CategoryName = categories.Name;
            exist.Id = product.Id;
            exist.IsAvailable = product.IsAvailable;
            var property12 = _productRepository.Update(exist);

            return new BaseResponse<ProductResponseModel>
            {
                Message = "product Updated Succesfully",
                Success = true,
                Data = new ProductResponseModel
                {
                    Price = product.Price,
                    IsAvailable = product.IsAvailable,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Id = product.Id,
                    Images = newList,
                    Name = product.Name,
                }
            };


        }
        
        public BaseResponse<ProductResponseModel> Delete(string Id)
        {
            var product = _productRepository.GetById(Id);
            var category = _cartegoryService.GetAll();

            if (product == null)
            {
                return new BaseResponse<ProductResponseModel>
                {
                    Message = "propertyId Not found",
                    Success = false,
                    Data = null,
                };
            }
            product.IsDeleted = true;
            product.IsAvailable = false;
            product.DeletedBy = _currentUser.GetCurrentUser();
            product.DateDeleted = DateTime.Now;
            var productDel = _productRepository.Update(product);

            return new BaseResponse<ProductResponseModel>
            {
                Message = "Deleted ",
                Success = true,
                Data = new ProductResponseModel
                {
                    Price = product.Price,
                    IsAvailable = product.IsAvailable,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    Id = product.Id,
                    Name = product.Name,
                }
            };
        }

        public BaseResponse<List<ProductResponseModel>> GetAll()
        {
           
            var products = _productRepository.GetAll();
            var category = _cartegoryService.GetAll();
            

            foreach (var item in products)
            {
                var get = _cartegoryService.GetById(item.CategoryId);

                item.CategoryName = get.Data.Name;

            }

            if (products == null)
            {
                return new BaseResponse<List<ProductResponseModel>>
                {
                    Message = "properties Not found",
                    Success = false,
                    Data = null,
                };
            }
            var listOfproduct = products.Select(product => new ProductResponseModel
            {
                Price = product.Price,
                IsAvailable = product.IsAvailable,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Id = product.Id,
                CurrentUser = _currentUser.GetCurrentUser(),
                CategoryName = product.CategoryName,
                CreatedBy = product.CreatedBy,
                DateCrated = product.DateCrated,
                Name = product.Name,
                Images = product.Images,
                
            }).ToList();
            

            return new BaseResponse<List<ProductResponseModel>>
            {
                Message = "All property",
                Success = true,
                Data = listOfproduct
            };
        }

        public BaseResponse<List<ProductResponseModel>> GetByAvailability()
        {
            var products = _productRepository.GetByAvailability();
            //var category = _cartegoryService.GetAll();
            foreach (var item in products)
            {
                var get = _cartegoryService.GetById(item.CategoryId);
                item.CategoryName = get.Data.Name;
            }
            if (products == null)
            {
                return new BaseResponse<List<ProductResponseModel>>
                {
                    Message = "No Available products",
                    Success = false,
                    Data = null,    
                };
            }
            var listOfproducts = products.Select(products => new ProductResponseModel
            {
                Price = products.Price,
                IsAvailable = products.IsAvailable,
                Description = products.Description,
                CategoryId = products.CategoryId,
                Id = products.Id,
                CurrentUser = _currentUser.GetCurrentUser(),
                CategoryName = products.CategoryName,
                CreatedBy = products.CreatedBy,
                DateCrated = products.DateCrated,
                Name = products.Name,
            }).ToList();
            return new BaseResponse<List<ProductResponseModel>>
            {
                Message = "All Product",
                Success = true,
                Data = listOfproducts
            };
        }

        public BaseResponse<List<ProductResponseModel>> GetByCartegoryId(string CartegoryId)
        {
            var exist = _productRepository.GetByCartegoryId(CartegoryId);
            var category = _cartegoryService.GetAll();
            foreach (var item in exist)
            {
                var get = _cartegoryService.GetById(item.CategoryId);
                item.CategoryName = get.Data.Name;
            }

            if (exist == null)
            {
                return new BaseResponse<List<ProductResponseModel>>
                {
                    Message = "cartegoryId not found",
                    Success = false,
                    Data = null,
                };
            }
            var products = _productRepository.GetByCartegoryId(CartegoryId);
            var listOfproducts = products.Select(products => new ProductResponseModel
            {
                Price = products.Price,
                IsAvailable = products.IsAvailable,
                Description = products.Description,
                CategoryId = products.CategoryId,
                Id = products.Id,
                CurrentUser = _currentUser.GetCurrentUser(),
                CategoryName = products.CategoryName,
                CreatedBy = products.CreatedBy,
                Name = products.Name,
                DateCrated = products.DateCrated,
                Images = products.Images,
            }).ToList();
            return new BaseResponse<List<ProductResponseModel>>
            {
                Message = "All Product",
                Success = true,
                Data = listOfproducts
            };
        }

        public BaseResponse<List<ProductResponseModel>> GetByDescription(string Description)
        {
            var products = _productRepository.GetByDescription(Description);
            var category = _cartegoryService.GetAll();
            foreach (var item in products)
            {
                var get = _cartegoryService.GetById(item.CategoryId);
                item.CategoryName = get.Data.Name;
            }
            if (products == null)
            {
                return new BaseResponse<List<ProductResponseModel>>
                {
                    Message = "Description not found",
                    Success = false,
                    Data = null,
                };
            }
            var listOfproducts = products.Select(products => new ProductResponseModel
            {
                Price = products.Price,
                IsAvailable = products.IsAvailable,
                Description = products.Description,
                CategoryId = products.CategoryId,
                Id = products.Id,
                CurrentUser = _currentUser.GetCurrentUser(),
                CategoryName = products.CategoryName,
                CreatedBy = products.CreatedBy,
                DateCrated = products.DateCrated,
                Name = products.Name,
                Images = products.Images,
            }).ToList();
            return new BaseResponse<List<ProductResponseModel>>
            {
                Message = "All Product",
                Success = true,
                Data = listOfproducts
            };
        }

        public BaseResponse<ProductResponseModel> GetById(string Id)
        {
            var exist = _productRepository.GetById(Id);
            var category = _cartegoryRepository.GetAll();
                var get = _cartegoryService.GetById(exist.CategoryId);
            exist.CategoryName = get.Data.Name;

            if (exist == null)
            {
                return new BaseResponse<ProductResponseModel>
                {
                    Message = "PropertyId not found",
                    Success = false,
                    Data = null,
                };
            }
            return new BaseResponse<ProductResponseModel>
            {
                Message = "Gotten the Property",
                Success = true,
                Data = new ProductResponseModel
                {
                    Price = exist.Price,
                    IsAvailable = exist.IsAvailable,
                    Description = exist.Description,
                    CategoryId = exist.CategoryId,
                    Id = exist.Id,
                    Name = exist.Name,
                     Images = exist.Images,
                }
            };
        }

        public BaseResponse<List<ProductResponseModel>> GetByPrice(double Price)
        {
            var products = _productRepository.GetByPrice(Price);
            var category = _cartegoryService.GetAll();
            foreach (var item in products)
            {
                var get = _cartegoryService.GetById(item.CategoryId);
                item.CategoryName = get.Data.Name;
            }
            if (products == null)
            {
                return new BaseResponse<List<ProductResponseModel>>
                {
                    Message = "Product with price not found",
                    Success = false,
                    Data = null,
                };
            }
            var listOfproducts = products.Select(products => new ProductResponseModel
            {
                Price = products.Price,
                IsAvailable = products.IsAvailable,
                Description = products.Description,
                CategoryId = products.CategoryId,
                Id = products.Id,
                CurrentUser = _currentUser.GetCurrentUser(),
                CategoryName = products.CategoryName,
                CreatedBy = products.CreatedBy,
                DateCrated = products.DateCrated,
                Name = products.Name,
                Images = products.Images,
            }).ToList();
            return new BaseResponse<List<ProductResponseModel>>
            {
                Message = "All Product",
                Success = true,
                Data = listOfproducts
            };
        }
    }

}