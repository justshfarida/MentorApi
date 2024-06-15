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
        public Task<ResponseModel<bool>> AssignRoletoUserAsync(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<CreateUserResponseDTO>> CreateAsync(CreateUserDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteUserAsync(string userIOorName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<UserGetDTO>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
