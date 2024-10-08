﻿using MentorApi.Abstractions.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace MentorApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController:ControllerBase
    {
        readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string usernameorEmail, string password)
        {
            var data=await authService.LoginAsync(usernameorEmail, password);
            return StatusCode(data.Status, data);
        }
        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken)
        {
            var data = await authService.LoginWithRefreshTokenAsync(refreshToken);
            return StatusCode(data.Status, data);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin, User")]
        public async Task<IActionResult> Logout(string usernameorEmail)
        {
            var data = await authService.LogOut(usernameorEmail);
            return StatusCode(data.Status, data);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin, User")]
        public async Task<IActionResult> ResetPassword(string email, string curPassword, string newPassword)
        {
            var data =await authService.ResetPassword(email, curPassword, newPassword); 
            return StatusCode(data.Status, data);
        }
    }
}
