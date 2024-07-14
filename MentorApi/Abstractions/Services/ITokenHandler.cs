using MentorApi.DTOs.TokenDTOs;
using MentorApi.Entities.Identity;

namespace MentorApi.Abstractions.Services
{
    public interface ITokenHandler
    {
        Task<TokenDTO> CreateToken(AppUser user);
        string CreateRefreshToken();
    }
}
