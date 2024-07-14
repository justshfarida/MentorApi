using MentorApi.Abstractions.Services;
using MentorApi.DTOs.UserDTOs;
using MentorApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace MentorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<UserGetDTO>>>> GetAll()
        {
            var response = await _userService.GetAllUsersAsync();
            return StatusCode(response.Status, response);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CreateUserResponseDTO>>> Create(CreateUserDTO user)
        {
            var response = await _userService.CreateAsync(user);
            return StatusCode(response.Status, response);
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseModel<bool>>> Delete(string user)
        {
            var response = await _userService.DeleteUserAsync(user);
            return StatusCode(response.Status, response);
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<bool>>> Update(UserUpdateDTO model)
        {
            var response = await _userService.UpdateUserAsync(model);
            return StatusCode(response.Status, response);
        }
        [HttpGet("GetRoles")]
        public async Task<ActionResult<ResponseModel<string>>> GetRolesTo(string userIdOrName)
        {
            var response = await _userService.GetRolesToUserAsync(userIdOrName);
            return StatusCode(response.Status, response);
        }
    //[HttpPost("AssignRoles")]
    
    }
}
