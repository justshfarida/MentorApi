using MentorApi.Abstractions.Services;
using MentorApi.Model;
using Microsoft.AspNetCore.Identity;
using MentorApi.Entities.Identity;

using Microsoft.EntityFrameworkCore;
namespace MentorApi.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager) 
        {
            _roleManager = roleManager;
        }
        public async Task<ResponseModel<bool>> CreateRole(string name)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data=false, Status=400};
            IdentityResult result = await  _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            if (result.Succeeded) 
            { 
                responseModel.Data= true;
                responseModel.Status = 201;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> DeleteRole(string id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data = false, Status = 400 };
            var role=await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result=await _roleManager.DeleteAsync(role);
                
                if (result.Succeeded) { 
                    responseModel.Data= true;
                    responseModel.Status = 200;
                }
            }
            return responseModel;
        }

        public async Task<ResponseModel<object>> GetAllroles()
        {
            ResponseModel<object> responseModel = new ResponseModel<object>() { Status = 400, Data = null };
            var roles=await _roleManager.Roles.ToListAsync();
            if(roles.Count()>0) 
            {
                responseModel.Data = roles;
                responseModel.Status = 200;
            }
            return responseModel;
        }

        public async Task<ResponseModel<object>> GetRoleById(string id)
        {
            ResponseModel<object> responseModel = new ResponseModel<object>() { Status = 400, Data = null };
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                responseModel.Data = role;
                responseModel.Status = 200;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateRole(string id, string name)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Status = 400, Data = false };
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null) 
            {
                role.Name = name;
                IdentityResult result= await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    responseModel.Data = true;
                    responseModel.Status = 200;
                }
            }
            return responseModel;
        }
    }
}
