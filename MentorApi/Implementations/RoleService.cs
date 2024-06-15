using MentorApi.Abstractions.Services;
using MentorApi.Model;
using Microsoft.AspNetCore.Identity;
using MentorApi.Entities.Identity;
namespace MentorApi.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager) 
        {
            _roleManager = roleManager;
        }
        public Task<ResponseModel<bool>> CreateRole(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteRole(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<object>> GetAllroles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<object>> GetRoleById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateRole(string id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
