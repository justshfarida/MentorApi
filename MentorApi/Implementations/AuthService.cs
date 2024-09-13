using MentorApi.Abstractions.Services;
using MentorApi.DTOs.TokenDTOs;
using MentorApi.Entities.Identity;
using MentorApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MentorApi.Implementations
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        readonly IConfiguration _configuration;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<ResponseModel<TokenDTO>> LoginAsync(string userNameorEmail, string password)
        {
            ResponseModel<TokenDTO> responseModel = new()
            { Data = null, Status = 400 };
            var user= await _userManager.FindByNameAsync(userNameorEmail);
            if (user == null)
            {
                user=await _userManager.FindByEmailAsync(userNameorEmail);

            }
            if (user == null)
            {
                responseModel.Status = 404; 
                return responseModel;
            }
            SignInResult result=await _signInManager.CheckPasswordSignInAsync(user, password, false);//bir nece defe sehv giris bas vererse, hesabi locklamamaq ucun bu parametri false edirik
            if (result.Succeeded)
            {
                TokenDTO tokenDTO = await _tokenHandler.CreateToken(user);
                var mins = Convert.ToDouble(_configuration["Token:RefreshTokenExpirationInMinutes"]);
                await _userService.UpdateRefreshToken(tokenDTO.RefreshToken, user , tokenDTO.ExpirationTime.AddMinutes(mins));
                responseModel.Data = tokenDTO;
                responseModel.Status = 200;
            }
            else
            {
                responseModel.Status = 401;
            }
            return responseModel;
        }

        public async Task<ResponseModel<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken)
        {
            ResponseModel<TokenDTO> responseModel = new() { Status=400, Data=null};
            AppUser user=await _userManager.Users.FirstOrDefaultAsync(x=>x.RefreshToken==refreshToken);
            if (user == null)
            {
                responseModel.Status = 404;
                return responseModel;
            }
            if(user.RefreshTokenEndTime>DateTime.UtcNow)
            {
                TokenDTO token=await _tokenHandler.CreateToken(user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user , token.ExpirationTime);
                responseModel.Data = token;
                responseModel.Status = 200;
            }
            else
            {
                responseModel.Status = 401;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> LogOut(string userNameorEmail)
        {
            ResponseModel<bool> responseModel = new() { Data = false, Status = 400 };
            AppUser user =await _userManager.FindByEmailAsync(userNameorEmail);
            
            if (user == null) { 
                user=await _userManager.FindByNameAsync(userNameorEmail);  
            }
            if (user==null)
            {
                responseModel.Status = 404;
                return responseModel;
            }
            user.RefreshToken = null;
            user.RefreshTokenEndTime = null;//refresh tokeni silmek lazimdi
             var result=await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();//bizde yoxdu
            if (result.Succeeded)
            {
                responseModel.Data = true;
                responseModel.Status = 200;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> ResetPassword(string email, string curPassword, string newPassword)
        {
            ResponseModel<bool> responseModel = new() { Data = false, Status = 400 };
            AppUser user=await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result=await _userManager.ChangePasswordAsync(user, curPassword, newPassword);
                if (result.Succeeded)
                {
                    responseModel.Data = true;
                    responseModel.Status=200;
                }
            }
            return responseModel;
        }
    }
}
