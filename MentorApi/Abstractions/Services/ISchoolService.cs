using MentorApi.DTOs.SchoolDTOs;
using MentorApi.DTOs.StudentDTOs;
using MentorApi.Model;

namespace MentorApi.Abstractions.Services
{
    public interface ISchoolService
    {
        Task<ResponseModel<List<SchoolGetDTO>>> GetAllSchools();
        Task<ResponseModel<SchoolCreateDTO>> CreateSchools(SchoolCreateDTO schoolCreate);
        Task<ResponseModel<bool>> DeleteSchools(int id);
        Task<ResponseModel<bool>> UpdateSchool(SchoolUpdateDTO schoolUpdate);
        Task<ResponseModel<SchoolGetDTO>> GetSchoolById(int id);
    }
}
