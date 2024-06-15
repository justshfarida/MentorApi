using MentorApi.Abstractions.Services;
using MentorApi.DTOs.StudentDTOs;
using MentorApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace MentorApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<StudentGetDTO>>>> GetAll()
        {
            var response = await _studentService.GetAllStudents();
            return StatusCode(response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<StudentGetDTO>>> GetById(int id)
        {
            var response = await _studentService.GetStudentById(id);
            return StatusCode(response.Status, response);

        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<StudentCreateDTO>>> Create(StudentCreateDTO studentCreateDTO)
        {
            var response = await _studentService.CreateStudent(studentCreateDTO);
            return StatusCode(response.Status, response);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> Update(int id, StudentUpdateDTO studentUpdateDTO)
        {
            var response = await _studentService.UpdateStudent(studentUpdateDTO, id);
            return StatusCode(response.Status, response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> Delete(int id)
        {
            var response = await _studentService.DeleteStudent(id);
            return StatusCode(response.Status, response);

        }

        [HttpGet("BySchoolId/{id}")]
        public async Task<ActionResult<ResponseModel<List<StudentGetDTO>>>> GetBySchoolId(int id)
        {
            var response = await _studentService.StudentsBySchoolId(id);
            return StatusCode(response.Status, response);

        }

        [HttpPost("ChangeSchool")]
        public async Task<ActionResult<ResponseModel<bool>>> Change(ChangeSchoolDTO model)
        {
            var response = await _studentService.ChangeSchool(model);
            return StatusCode(response.Status, response);

        }

        [HttpPut("ChangeSchool/{studentId}/{newSchoolId}")]
        public async Task<ActionResult<ResponseModel<bool>>> Change(int studentId, int newSchoolId)
        {
            var response = await _studentService.ChangeSchool(studentId, newSchoolId);
            return StatusCode(response.Status, response);

        }
        [HttpGet]
        public int Get()
        {
            throw new NotImplementedException("MEOW");
        }
    }
}
