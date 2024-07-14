using AutoMapper;
using MentorApi.Abstractions.IUnitOfWorks;
using MentorApi.Abstractions.Services;
using MentorApi.DTOs.StudentDTOs;
using MentorApi.Entities.AppdbContextEntity;
using MentorApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MentorApi.Implementations
{

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        //private IRepository<Student> _student;
        //private IRepository<School> _school;
        public StudentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            try
            {
                var data = await unitOfWork.GetRepository<Student>().GetByIDAsync(studentId);

                if (data != null)
                {
                    var schoolData=await unitOfWork.GetRepository<School>().GetByIDAsync(newSchoolId);
                    if(schoolData != null)
                    {
                        data.SchoolID = newSchoolId;
                        var rowsAffected = await unitOfWork.SaveChangesAsync();
                        if (rowsAffected > 0)
                        {
                            responseModel.Data = true;
                            responseModel.Status = 200;
                        }
                        else
                        {
                            responseModel.Data = false;
                            responseModel.Status = 404;
                        }
                    }
                }
                else
                {
                    responseModel.Data = false;
                    responseModel.Status = 500;
                }

            }
            catch (Exception ex)
            {
                responseModel.Data = false;
                responseModel.Status = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> ChangeSchool(ChangeSchoolDTO model)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>();
            try
            {

                if (model != null)

                {
                    var data = await unitOfWork.GetRepository<Student>().GetByIDAsync(model.StudentID);
                    var schoolData=await unitOfWork.GetRepository<School>().GetByIDAsync(data.SchoolID);
                    if(schoolData != null)
                    {
                        data.SchoolID = model.NewSchoolID;
                        int rowsAffected = await unitOfWork.SaveChangesAsync();
                        if (rowsAffected > 0)
                        {
                            responseModel.Data = true;
                            responseModel.Status = 200;
                        }
                        else
                        {
                            responseModel.Data = false;
                            responseModel.Status = 404;
                        }
                    }
                }
                else
                {
                    responseModel.Data = false;
                    responseModel.Status = 500;
                }
            }
            catch (Exception ex)
            {
                responseModel.Data = false;
                responseModel.Status = 500;
            }
            return responseModel;
        }
    
        public async Task<ResponseModel<StudentCreateDTO>> CreateStudent(StudentCreateDTO studentCreateDTO)
        {
            ResponseModel<StudentCreateDTO> responseModel = new ResponseModel<StudentCreateDTO>() { Data=null, Status=400};

            try
            {
                if (studentCreateDTO != null)
                {
                    var data = _mapper.Map<Student>(studentCreateDTO);
                    await unitOfWork.GetRepository<Student>().AddAsync(data);
                    int rowsAffected = await unitOfWork.SaveChangesAsync();
                    if (rowsAffected > 0)
                    {
                        responseModel.Data = studentCreateDTO;
                        responseModel.Status = 201;//only create case
                    }
                    else
                    {
                        responseModel.Status = 500;
                    }
                }
            }
            catch(Exception ex)
            {
                responseModel.Status = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> DeleteStudent(int id)
        {
            ResponseModel<bool> responseModel = new ResponseModel<bool>() { Data=false, Status=500};
            try
            {
                var data = await unitOfWork.GetRepository<Student>().GetByIDAsync(id);
                if (data!=null)
                {
                    unitOfWork.GetRepository<Student>().Remove(data);
                    int rowsAffected= await unitOfWork.SaveChangesAsync();
                    if(rowsAffected > 0)
                    {
                        responseModel.Data = true;
                        responseModel.Status = 200;
                    }
                }
                else
                {
                    responseModel.Status = 400;
                }
            }
            catch(Exception ex)
            {

                responseModel.Data = false;
                responseModel.Status = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<StudentGetDTO>>> GetAllStudents()
        {
            ResponseModel < List<StudentGetDTO> > responseModel= new ResponseModel<List<StudentGetDTO>>() { Data=null, Status=500};
            try
            {
                var students= await unitOfWork.GetRepository<Student>().GetAll().Include(x=>x.School).ToListAsync();
                var studentDTOs=_mapper.Map<List<StudentGetDTO>>(students);
                responseModel.Data = studentDTOs;
                responseModel.Status = 200;
            }
            catch( Exception ex )
            {
                responseModel.Data = null;
                responseModel.Status = 500;
            }
            return responseModel;

        }

        public async Task<ResponseModel<StudentGetDTO>> GetStudentById(int id)
        {
            ResponseModel<StudentGetDTO> responseModel= new ResponseModel<StudentGetDTO>() { Status=500, Data=null};
            try
            {
                var data = await unitOfWork.GetRepository<Student>().GetByIDAsync(id);
                if (data != null)
                {
                    await unitOfWork.GetRepository<School>().GetByIDAsync(data.SchoolID);//ToDo gor isleyir? data.School?
                    var studentDTO = _mapper.Map<StudentGetDTO>(data);
                    responseModel.Status = 200;
                    responseModel.Data = studentDTO;
                }
                else
                {
                    responseModel.Status = 404;
                }
            }   
            catch (Exception ex)
            {
                responseModel.Status = 500;
                responseModel.Data = null;
            }
            return responseModel;
        }

        public async Task<ResponseModel<List<StudentGetDTO>>> StudentsBySchoolId(int id)
        {
            ResponseModel<List<StudentGetDTO>> responseModel = new ResponseModel<List<StudentGetDTO>>
            {
                Data = null,
                Status = 500,
            };
            try
            {
                var data= await unitOfWork.GetRepository<School>().GetByIDAsync(id);
                if(data != null)
                {
                    var students = unitOfWork.GetRepository<Student>().GetAll().Where(x=>x.SchoolID ==data.Id).Include(x=>x.School);//ToDo to list yazmali ola bilerik (iqueryable a gore)
                    var studentDTOs=_mapper.Map<List<StudentGetDTO>>(students);
                    return new ResponseModel<List<StudentGetDTO>> 
                    { 
                    Data=studentDTOs,
                    Status=200,};
                }
                else
                {
                    responseModel.Status = 404;
                }
            }
            catch(Exception ex)
            {

                responseModel.Data = null;
                responseModel.Status = 500;
                
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateStudent(StudentUpdateDTO studentUpdateDTO, int id)
        {

            try
            {
                var data = await unitOfWork.GetRepository<Student>().GetByIDAsync(id);
                if (data != null)
                {
                    var student = _mapper.Map(studentUpdateDTO, data);
                    int rowsAffected = await unitOfWork.SaveChangesAsync();
                    if (rowsAffected > 0)
                    {
                        return new ResponseModel<bool>
                        {
                            Data = true,
                            Status = 200,
                        };

                    }
                    else
                    {
                        return new ResponseModel<bool> { Data = false, Status = 500 };
                    }

                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool> { Status = 500, Data = false, };
            }
            return new ResponseModel<bool> { Status = 500, Data = false, };

        }
    }
}
