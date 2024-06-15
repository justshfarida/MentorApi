using MentorApi.DTOs.UserDTOs;
using MentorApi.Entities.Identity;
using MentorApi.Model;

namespace MentorApi.Abstractions.Services
{
    public interface IUserService
    {
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate); 
        public Task<ResponseModel<bool>> AssignRoletoUserAsync(string userId, string[] roles);
        Task<ResponseModel<CreateUserResponseDTO>> CreateAsync(CreateUserDTO model);
        public Task<ResponseModel<List<UserGetDTO>>> GetAllUsersAsync();
        public Task<ResponseModel<string[]>> GetRolesToUserAsync (string userIdOrName);
        public Task<ResponseModel<bool>> DeleteUserAsync(string userIOorName);
        public Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model);


    }
}
