using MentorApi.DTOs.SchoolDTOs;
using MentorApi.DTOs.StudentDTOs;
using MentorApi.Model;

namespace MentorApi.Abstractions.Services
{
    public interface IStudentService
    {
        Task<ResponseModel<List<StudentGetDTO>>> GetAllStudents();
        Task<ResponseModel<StudentCreateDTO>> CreateStudent(StudentCreateDTO studentCreateDTO);
        Task<ResponseModel<bool>> UpdateStudent(StudentUpdateDTO studentUpdateDTO, int id);
        Task<ResponseModel<bool>> DeleteStudent(int id);
        Task<ResponseModel<StudentGetDTO>> GetStudentById(int id);
        Task<ResponseModel<List<StudentGetDTO>>> StudentsBySchoolId(int id);
        Task<ResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId);
        Task<ResponseModel<bool>> ChangeSchool(ChangeSchoolDTO model);
    }
}
