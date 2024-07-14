using MentorApi.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentorApi.Controllers;

    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _roleService.GetAllroles();
            return StatusCode(response.Status, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _roleService.GetRoleById(id);
            return StatusCode(response.Status, response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var response=await _roleService.CreateRole(name);
            return StatusCode(response.Status, response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response =await _roleService.DeleteRole(id);
            return StatusCode(response.Status, response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string name, string id)
        {
            var response =await _roleService.UpdateRole(id, name);
            return StatusCode(response.Status, response);
        }

    }
