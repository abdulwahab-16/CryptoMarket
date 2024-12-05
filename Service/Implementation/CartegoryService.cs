using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Entities;
using RealEstateMvc.Models.Enum;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class CartegoryService : ICartegoryService
    {
        private readonly ICartegoryRepository _cartegoryRepository;
        private readonly ICurrentUser _currentUser;
        public CartegoryService(ICartegoryRepository cartegoryRepository, ICurrentUser currentUser)
        {
            _cartegoryRepository = cartegoryRepository;
            _currentUser = currentUser;
        }
        public BaseResponse<CategoryResponseModel> Create(CategoryRequestModel cartegory)
        {
            var check = _cartegoryRepository.Check(cartegory.Name);
            if (check)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Message = "CartegoryName Already exist",
                    Success = false,
                    Data = null
                };
            }
            Category cartegory1 = new Category
            {
                Name = cartegory.Name,
                Description = cartegory.Description,
                IsDeleted = false,
                CreatedBy = _currentUser.GetCurrentUser(),
            };
            _cartegoryRepository.Create(cartegory1);

            return new BaseResponse<CategoryResponseModel>
            {
                Message = "Cartegory Created Succesfully",
                Success = true,
                Data = new CategoryResponseModel
                {
                    Id = cartegory1.Id,
                    Name = cartegory1.Name,
                    Description = cartegory1.Description,
                }
            };
        }

        public BaseResponse<CategoryResponseModel> Delete(string Name)
        {
            var category = _cartegoryRepository.GetByName(Name);
            if (category.Products.Count != 0)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Message = "Existing product in the category",
                    Success = false,
                    Data = null,
                };
            }
            if (category == null)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Message = "CartegoryName Not found",
                    Success = false,
                    Data = null,
                };
            }
            category.IsDeleted = true;
            category.DateCrated = DateTime.Now;
            category.DeletedBy = _currentUser.GetCurrentUser();

            var newcartegory = _cartegoryRepository.Update(category);
            return new BaseResponse<CategoryResponseModel>
            {
                Message = "Deleted ",
                Success = true,
                Data = new CategoryResponseModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                
                }
            };


        }

        public BaseResponse<List<CategoryResponseModel>> GetAll()
        {

            var cartegorys = _cartegoryRepository.GetAll();
            if (cartegorys == null)
            {
                return new BaseResponse<List<CategoryResponseModel>>
                {
                    Message = "no Cartegorys Available",
                    Success = false,
                    Data = null,
                };
            }
            var listOfcartegorys = cartegorys.Select(x => new CategoryResponseModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                products = x.Products.Select(p => new ProductResponseModel
                {
                    Price = p.Price,
                    IsAvailable = p.IsAvailable,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                }).ToList()
            }).ToList();
            return new BaseResponse<List<CategoryResponseModel>>
            {
                Message = "All categories",
                Success = true,
                Data = listOfcartegorys
            };
        }

        public BaseResponse<CategoryResponseModel> GetById(string Id)
        {
            var cartegory = _cartegoryRepository.GetById(Id);
            if (cartegory == null)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Message = "Cartegory not found",
                    Success = false,
                    Data = null,
                };
            }
            return new BaseResponse<CategoryResponseModel>
            {
                Message = "Gotten the Cartegory",
                Success = true,
                Data = new CategoryResponseModel
                {
                    Id = cartegory.Id,
                    Name = cartegory.Name,
                    Description = cartegory.Description,
                    products = cartegory.Products.Select(p => new ProductResponseModel
                    {
                        Price = p.Price,
                        IsAvailable = p.IsAvailable,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        CategoryName = p.CategoryName,
                    }).ToList()
                }
            };
        }

        public BaseResponse<CategoryResponseModel> GetByName(string Name)
        {
            var exist = _cartegoryRepository.Check(Name);
            if (!exist)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Message = "CartegoryName not found",
                    Success = false,
                    Data = null,
                };
            }
            var cartegory = _cartegoryRepository.GetByName(Name);
            return new BaseResponse<CategoryResponseModel>
            {
                Message = "Gotten the Cartegory",
                Success = true,
                Data = new CategoryResponseModel
                {
                    Id = cartegory.Id,
                    Name = cartegory.Name,
                    Description = cartegory.Description,
                    products = cartegory.Products.Select(p => new ProductResponseModel
                    {
                        Price = p.Price,
                        IsAvailable = p.IsAvailable,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        CategoryName = p.CategoryName,
                    }).ToList()
                }
            };
        }

        public BaseResponse<CategoryResponseModel> Update(CategoryRequestModel cartegory)
        {
            var get = _cartegoryRepository.GetByName(cartegory.Name);
            if (get == null)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Message = "CartegoryName not found",
                    Success = false,
                    Data = null,
                };
            }

            get.Description = cartegory.Description;
            get.Name = cartegory.Name;
            get.CreatedBy = _currentUser.GetCurrentUser();


            var newcartegory = _cartegoryRepository.Update(get);

            return new BaseResponse<CategoryResponseModel>
            {
                Message = "Updated the Cartegory",
                Success = true,
                Data = new CategoryResponseModel
                {
                    Id = newcartegory.Id,
                    Name = newcartegory.Name,
                    Description = newcartegory.Description,
                    products = newcartegory.Products.Select(p => new ProductResponseModel
                    {
                        Price = p.Price,
                        IsAvailable = p.IsAvailable,
                        Description = p.Description,
                        CategoryId = p.CategoryId,
                        CategoryName = p.CategoryName,
                    }).ToList()
                }
            };
        }

    }
}