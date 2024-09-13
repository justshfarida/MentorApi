using MentorApi.DTOs.TokenDTOs;
using MentorApi.Model;

namespace MentorApi.Abstractions.Services
{
    public interface IAuthService
    {
        Task<ResponseModel<TokenDTO>> LoginAsync(string userNameorEmail, string password);
        Task<ResponseModel<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken);
        Task<ResponseModel<bool>> LogOut(string userNameorEmail);
        Task<ResponseModel<bool>> ResetPassword(string email, string curPassword, string newPassword);

    }
}
