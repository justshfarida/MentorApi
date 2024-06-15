using MentorApi.Model;

namespace MentorApi.Abstractions.Services
{
    public interface IRoleService
    {
        Task<ResponseModel<object>> GetAllroles();
        Task<ResponseModel<object>> GetRoleById(string id);
        Task<ResponseModel<bool>> CreateRole(string name);
        Task<ResponseModel<bool>> DeleteRole(string id);
        Task<ResponseModel<bool>> UpdateRole(string id, string name);


    }
}

