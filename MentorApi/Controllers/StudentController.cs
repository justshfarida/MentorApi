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
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<StudentGetDTO>>> GetById(int id)
        {
            var response = await _studentService.GetStudentById(id);
            if (response.Data == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<StudentCreateDTO>>> Create(StudentCreateDTO studentCreateDTO)
        {
            var response = await _studentService.CreateStudent(studentCreateDTO);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> Update(int id, StudentUpdateDTO studentUpdateDTO)
        {
            var response = await _studentService.UpdateStudent(studentUpdateDTO, id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> Delete(int id)
        {
            var response = await _studentService.DeleteStudent(id);
            return Ok(response);
        }

        [HttpGet("BySchoolId/{id}")]
        public async Task<ActionResult<ResponseModel<List<StudentGetDTO>>>> GetBySchoolId(int id)
        {
            var response = await _studentService.StudentsBySchoolId(id);
            return Ok(response);
        }

        [HttpPost("ChangeSchool")]
        public async Task<ActionResult<ResponseModel<bool>>> Change(ChangeSchoolDTO model)
        {
            var response = await _studentService.ChangeSchool(model);
            return Ok(response);
        }

        [HttpPut("ChangeSchool/{studentId}/{newSchoolId}")]
        public async Task<ActionResult<ResponseModel<bool>>> Change(int studentId, int newSchoolId)
        {
            var response = await _studentService.ChangeSchool(studentId, newSchoolId);
            return Ok(response);
        }
        [HttpGet]
        public int Get()
        {
            throw new NotImplementedException("MEOW");
        }
    }
}
