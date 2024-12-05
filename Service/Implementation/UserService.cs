using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RealEstate_Mvc_.Context;
using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Models.Entities;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Entities;
using System.Collections.Generic;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRoleRepository _roleRepository;
        private readonly ContextClass _contextClass;
        public UserService(IUserRepository userRepository, ICurrentUser currentUser, IRoleRepository roleRepository,ContextClass contextClass)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _roleRepository = roleRepository;
            _contextClass = contextClass;
        }

        public BaseResponse<UserResponseModel> Create(UserRequestModel user)
        {
            var Check = _userRepository.Check(user.Email);
            var role = _roleRepository.GetByName(user.RoleName);
            if (Check)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "user Email already exist found",
                    Success = false,
                    Data = null,
                };
            }
            var newPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            if (user.Password != user.ConfirmPassword)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "password does not correspond",
                    Success = false,
                    Data = null,
                };
            }
            User newUser = new User
            {
                Email = user.Email,
                CreatedBy = _currentUser.GetCurrentUser(),
                FullName = user.FullName,
                Password = newPassword,
                //UserRoles = new List<UserRole>(),


            };
            if (role == null)
            {

                Role newRole = new Role
                {
                    Name = user.RoleName,
                    Description = user.RoleDescription,
                    IsDeleted = false,
                    CreatedBy = _currentUser.GetCurrentUser(),
                };
                string RoleId = newRole.Id;
                newUser.UserRoles.Add(new UserRole
                {
                    User = newUser,
                    RoleId = newRole.Id,
                    Role = newRole,
                    UserId = newUser.Id
                });
                _roleRepository.Create(newRole);
            }
            newUser.UserRoles.Add(new UserRole
            {
                User = newUser,
                RoleId = role.Id,
                Role = role,
                UserId = newUser.Id


            });
            _userRepository.Create(newUser);
            return new BaseResponse<UserResponseModel>
            {
                Message = "user Created",
                Success = true,
                Data = new UserResponseModel
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    Id = newUser.Id,
                    RoleName = user.RoleName,
                }
            };
        }

        public BaseResponse<UserResponseModel> Delete(string Email)
        {
            var user = _userRepository.Get(Email);
            if (user == null)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "user Email Not found",
                    Success = false,
                    Data = null,
                };
            }
            user.IsDeleted = true;
            user.DeletedBy = _currentUser.GetCurrentUser();
            user.DateDeleted = DateTime.Now;
            var newUser = _userRepository.Update(user);

            return new BaseResponse<UserResponseModel>
            {
                Message = "Deleted ",
                Success = true,
                Data = new UserResponseModel
                {
                    Email = user.Email,
                    FullName = user.FullName,

                }
            };
        }

        public BaseResponse<UserResponseModel> Get(string email)
        {
            var exist = _userRepository.Check(email);
            if (!exist)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "user Email not found",
                    Success = false,
                    Data = null,
                };
            }
            var user = _userRepository.Get(email);
            return new BaseResponse<UserResponseModel>
            {
                Message = "Gotten the user",
                Success = true,
                Data = new UserResponseModel
                {
                    Email = user.Email,
                    FullName = user.FullName,
                }
            };
        }

        public BaseResponse<List<UserResponseModel>> GetAll()
        {
            var users = _userRepository.GetAll();
            if (users == null)
            {
                return new BaseResponse<List<UserResponseModel>>
                {
                    Message = " no users Available",
                    Success = false,
                    Data = null,
                };
            }
            var listOfUser = users.Select(user => new UserResponseModel
            {
                Email = user.Email,
                FullName = user.FullName,
            }).ToList();
            return new BaseResponse<List<UserResponseModel>>
            {
                Message = "All users",
                Success = true,
                Data = listOfUser
            };
        }

        public BaseResponse<List<UserResponseModel>> GetByRoleId(string Id)
        {

            var exist = _userRepository.GetByRoleId(Id);
            if (exist == null)
            {
                return new BaseResponse<List<UserResponseModel>>
                {
                    Message = "users not found",
                    Success = false,
                    Data = null,
                };
            }
            var user = _userRepository.GetByRoleId(Id);
            var listOfUser = user.Select(user => new UserResponseModel
            {
                Email = user.Email,
                FullName = user.FullName,
            }).ToList();
            return new BaseResponse<List<UserResponseModel>>
            {
                Message = "Gotten the user",
                Success = true,
                Data = listOfUser,
            };
        }

        public BaseResponse<UserResponseModel> Login(UserLoginRequestModel userLoginRequestModel)
        {
            var exist = _userRepository.Check(userLoginRequestModel.Email);
            if (!exist)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "user Email not found",
                    Success = false,
                    Data = null,
                };
            }
            var user = _userRepository.Get(userLoginRequestModel.Email);
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(userLoginRequestModel.Password, user.Password);
            if (isPasswordValid)
            {
              
                    // Fetch the user's role
                    var role = _contextClass.UserRoles
                        .Where(ur => ur.UserId == user.Id)
                        .Select(ur => ur.Role.Name)
                        .FirstOrDefault();
                return new BaseResponse<UserResponseModel>
                {
                    Message = "Login successful",
                    Success = true,
                    Data = new UserResponseModel
                    {
                        Email = user.Email,
                        FullName = user.FullName,
                        RoleName = role
                    }
                };
            }
            return new BaseResponse<UserResponseModel>
            {
                Message = "Password does not match",
                Success = false,
                Data = null
            };
        }

        public BaseResponse<UserResponseModel> Update(UserRequestModel userRequestModel)
        {
            var exist = _userRepository.Get(userRequestModel.Email);
            if (exist == null)
            {
                return new BaseResponse<UserResponseModel>
                {
                    Message = "user Email not found",
                    Success = false,
                    Data = null,
                };
            }
            var newPassword = $"qwert{BCrypt.Net.BCrypt.HashPassword(userRequestModel.Password)}";

            exist.Email = userRequestModel.Email;
            exist.Password = newPassword;
            exist.FullName = userRequestModel.FullName;
            exist.CreatedBy = _currentUser.GetCurrentUser();
            var updatedUser = _userRepository.Update(exist);



            return new BaseResponse<UserResponseModel>
            {
                Message = "Updated the user",
                Success = true,
                Data = new UserResponseModel
                {
                    Email = exist.Email,
                    FullName = exist.FullName,
                }
            };
        }
    }
}
