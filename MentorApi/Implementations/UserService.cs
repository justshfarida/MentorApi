using AutoMapper;
using MentorApi.Abstractions.Services;
using MentorApi.DTOs.UserDTOs;
using MentorApi.Entities.Identity;
using MentorApi.Model;
using Microsoft.AspNetCore.Identity;

namespace MentorApi.Implementations
{
    public class UserService : IUserService

    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ResponseModel<bool>> AssignRoletoUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            ResponseModel<bool> responseModel = new ResponseModel<bool>
            {
                Data = false,
                Status = 400 
            };
            if (user != null) 
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                IdentityResult result = await _userManager.AddToRolesAsync(user, roles);
                if (result.Succeeded)
                {
                    responseModel.Data = true;
                    responseModel.Status = 200; 
                }
            }
            return responseModel;
        }

        public async Task<ResponseModel<CreateUserResponseDTO>> CreateAsync(CreateUserDTO model)
        {
            ResponseModel<CreateUserResponseDTO> responseModel = new ResponseModel<CreateUserResponseDTO>()
            { Data = null,
                Status = 400 };

            CreateUserResponseDTO responseDTO = new CreateUserResponseDTO()
            {
                Message = "User not created",
                Succeeded = false
            };
            if (model != null)
            {
                var user = _mapper.Map<AppUser>(model);//Mapper lazim burda yoxsa elle maplemeliyik?
                user.Id = Guid.NewGuid().ToString(); // Ensure Id is set
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    responseDTO.Message = string.Join("\n", result.Errors.Select(error => $"{error.Code}-{error.Description}"));
                }

                responseDTO.Message = "User Successfully created";
                responseDTO.Succeeded = true;


                responseModel.Data = responseDTO;
                responseModel.Status = 201;
            }
            AppUser _user = await _userManager.FindByNameAsync(model.UserName);
            if (_user == null) {
                _user =await _userManager.FindByEmailAsync(model.Email);
                if (_user == null)
                {
                    _user = await _userManager.FindByIdAsync(_user.Id);

                }

            }
            if (_user != null)
                await _userManager.AddToRoleAsync(_user, "User");

            return responseModel;

        }

        public async Task<ResponseModel<bool>> DeleteUserAsync(string userIOorName)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>()
            {
                Data = false,
                Status = 500
            };
            AppUser user = await _userManager.FindByIdAsync(userIOorName);
            if (user == null)
            { 
               user =await _userManager.FindByNameAsync(userIOorName);
            }
            if (user==null)
            {
                responseModel.Status = 404; // Not Found

            }
            IdentityResult result =await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                responseModel.Data = true;
                responseModel.Status = 200;
            }
            else {
                responseModel.Status = 400;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<UserGetDTO>>> GetAllUsersAsync()
        {
            List<AppUser> users = _userManager.Users.ToList();
            ResponseModel<List<UserGetDTO>> responseModel=new ResponseModel<List<UserGetDTO>>() { Data = null, Status = 500 };
            if (users.Count > 0 && users!=null)
            {
                List<UserGetDTO> userGetDTOs = _mapper.Map<List<UserGetDTO>>(users);
                responseModel.Data = userGetDTOs;
                responseModel.Status = 200;
            }
            return responseModel;
        }

        public async Task<ResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            ResponseModel<string[]> responseModel = new ResponseModel<string[]>() { Data = null, Status=500 };
            var user =await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user != null) { 
                var userRoles= await _userManager.GetRolesAsync(user);
                responseModel.Data = userRoles.ToArray();
                responseModel.Status=200;
            }
            return responseModel;

        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime= accesTokenDate;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model)
        {
            var responseModel = new ResponseModel<bool>
            {
                Data = false,
                Status = 500
            };

            if (model != null)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    responseModel.Status = 404;
                }
                else
                {
                    // Map updated fields
                    user = _mapper.Map<AppUser>(model);

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        responseModel.Data = true;
                        responseModel.Status = 200;
                    }
                }
            }
            return responseModel;
        }
    }
}
