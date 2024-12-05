

using RealEstate_Mvc_.Dtos;
using RealEstate_Mvc_.Repository.Interface;
using RealEstate_Mvc_.Service.Interface;
using RealEstateMvc.Models.Entities;

namespace RealEstate_Mvc_.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ICurrentUser _currentUser;

        public RoleService(IRoleRepository roleRepository, ICurrentUser currentUser)
        {
            _roleRepository = roleRepository;
            _currentUser = currentUser;
        }
        public BaseResponse<RoleResponseModel> Create(RoleRequestModel role)
        {
            var roleExist = _roleRepository.Check(role.Name);
            if (roleExist)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Message = "role Name alraedy existing",
                    Success = false,
                    Data = null,
                };
            }
            Role newRole = new Role
            {
                Name = role.Name,
                Description = role.Description,
                IsDeleted = false,
                CreatedBy = _currentUser.GetCurrentUser(),
            };
            _roleRepository.Create(newRole);
            return new BaseResponse<RoleResponseModel>
            {
                Message = "role Created Succesfully",
                Success = true,
                Data = new RoleResponseModel
                {
                    Name = role.Name,
                    Description = role.Description,
                }
            };

        }

        public BaseResponse<RoleResponseModel> Delete(string Name)
        {
            var role = _roleRepository.GetByName(Name);
            if (role == null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Message = "role Name not found",
                    Success = false,
                    Data = null,
                };
            }
            if (role.UserRoles != null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Message = "role cannot be deleted",
                    Success = false,
                    Data = null,
                };
            }

            role.IsDeleted = true;
            role.DateDeleted = DateTime.Now;
            role.DeletedBy = _currentUser.GetCurrentUser();
          
            _roleRepository.Update(role);

            return new BaseResponse<RoleResponseModel>
            {
                Message = "Deleted ",
                Success = true,
                Data = new RoleResponseModel
                {
                    Name = role.Name,
                    Description = role.Description,
                    UserRoles = role.UserRoles,
                }
            };

        }

        public BaseResponse<List<RoleResponseModel>> GetAll()
        {


            var roles = _roleRepository.GetAll();
            if (roles == null)
            {
                return new BaseResponse<List<RoleResponseModel>>
                {
                    Message = "roles not found",
                    Success = false,
                    Data = null,
                };
            }
            var listOfrole = roles.Select(x => new RoleResponseModel
            {
                Name = x.Name,
                Description = x.Description,

            }).ToList();
            return new BaseResponse<List<RoleResponseModel>>
            {
                Message = "All roles",
                Success = true,
                Data = listOfrole
            };

        }

        public BaseResponse<RoleResponseModel> GetByName(string Name)
        {


            var roleExist = _roleRepository.Check(Name);


            if (!roleExist)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Message = "role Name not found",
                    Success = false,
                    Data = null,
                };
            }
            var role = _roleRepository.GetByName(Name);

            return new BaseResponse<RoleResponseModel>
            {
                Message = "Gotten the role",
                Success = true,
                Data = new RoleResponseModel
                {
                    Name = role.Name,
                    Description = role.Description,
                }
            };




        }

        public BaseResponse<RoleResponseModel> Update(RoleRequestModel role)
        {
            var roleExist = _roleRepository.GetByName(role.Name);
            if (roleExist == null)
            {
                return new BaseResponse<RoleResponseModel>
                {
                    Message = "role  Name not found",
                    Success = false,
                    Data = null,
                };
            }

            roleExist.Name = role.Name;
            roleExist.Description = role.Description;
            roleExist.CreatedBy = _currentUser.GetCurrentUser();



            var role1 = _roleRepository.Update(roleExist);



            return new BaseResponse<RoleResponseModel>
            {
                Message = "Updated the role",
                Success = true,
                Data = new RoleResponseModel
                {
                    Name = role1.Name,
                    Description = role1.Description,
                }
            };

        }
    }

}