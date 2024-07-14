using MentorApi.Abstractions.Services;
using MentorApi.DTOs.SchoolDTOs;
using MentorApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace MentorApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<SchoolGetDTO>>>> GetAll()
        {
            var response = await _schoolService.GetAllSchools();
            return StatusCode(response.Status, response);

        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<SchoolCreateDTO>>> Create(SchoolCreateDTO schoolCreate)
        {
            var response = await _schoolService.CreateSchools(schoolCreate);
            return StatusCode(response.Status, response);// This is equivalent to : return new ObjectResult(response) { StatusCode = response.StatusCode };


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> Delete(int id)
        {
            var response = await _schoolService.DeleteSchools(id);
            return StatusCode(response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> Update(SchoolUpdateDTO schoolUpdate)
        {
            var response = await _schoolService.UpdateSchool(schoolUpdate);
            return StatusCode(response.Status, response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<SchoolGetDTO>>> GetById(int id)
        {
            var response = await _schoolService.GetSchoolById(id);
            return StatusCode(response.Status, response);
        }
    }
}
