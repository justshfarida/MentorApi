using AutoMapper;
using MentorApi.Abstractions.Services;
using MentorApi.Context;
using MentorApi.DTOs.SchoolDTOs;
using MentorApi.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MentorApi.Entities.AppdbContextEntity;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.WebSockets;

namespace MentorApi.Implementations
{
    public class SchoolService : ISchoolService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;
        public SchoolService(SchoolDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ResponseModel<SchoolCreateDTO>> CreateSchools(SchoolCreateDTO schoolCreate)
        {
            ResponseModel<SchoolCreateDTO> responseModel = new ResponseModel<SchoolCreateDTO>();

            try
            {
                if(schoolCreate != null)
                {
                    var school = _mapper.Map<School>(schoolCreate);
                    await _dbContext.Schools.AddAsync(school);
                    var rowsAffected = await _dbContext.SaveChangesAsync();
                    if (rowsAffected > 0)
                    {
                        responseModel.Data = schoolCreate;
                        responseModel.Status = 200;
                    }
                    else
                    {
                        responseModel.Data = null;
                        responseModel.Status = 500;
                    }
                }
            }
            catch (Exception ex)
            {
                responseModel.Data = null;
                responseModel.Status = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> DeleteSchools(int id)
        {
            try
            {
                var data =  await _dbContext.Schools.FirstOrDefaultAsync(school => school.Id == id);
                if (data != null)
                {
                    _dbContext.Schools.Remove(data);
                    var rowsAffected = await _dbContext.SaveChangesAsync();
                    if(rowsAffected>0)
                    {
                        //Qisa yazilis?
                        return new ResponseModel<bool>
                        {
                            Data = true,
                            Status = 200,
                        };
                    }
                else
                {
                        return new ResponseModel<bool>
                        {
                            Data = false,
                            Status = 400,
                        };
                }
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Status = 500,
                };
            }
            return new ResponseModel<bool>
            {
                Data = false,
                Status = 500,
            };

        }
        public async Task<ResponseModel<List<SchoolGetDTO>>> GetAllSchools()
        {
            var responseModel = new ResponseModel<List<SchoolGetDTO>>(); // Initialize the response model
            try
            {
                var schools=await _dbContext.Schools.ToListAsync();
                var schoolDTOs=_mapper.Map<List<SchoolGetDTO>>(schools);
                responseModel.Data = schoolDTOs;
                responseModel.Status = 200;

            }
            catch(Exception ex)
            {
                responseModel.Data = null;
                responseModel.Status = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<SchoolGetDTO>> GetSchoolById(int id)
        {
            ResponseModel <SchoolGetDTO> responseModel = new ResponseModel<SchoolGetDTO>();
            try
            {
               var data= await _dbContext.Schools.FirstOrDefaultAsync(school=>school.Id==id);
               var schoolGetDTO=_mapper.Map<SchoolGetDTO>(data);
                responseModel.Data = schoolGetDTO;
                responseModel.Status = 200;

            }
            catch(Exception ex)
            {
                responseModel.Data = null;
                responseModel.Status = 500;
            }
            return responseModel;
        }

        public async Task<ResponseModel<bool>> UpdateSchool(SchoolUpdateDTO schoolUpdate)
        {
            var responseModel = new ResponseModel<bool>();
            try
            {
                if (schoolUpdate.Id > 0)
                {
                    var data = await _dbContext.Schools.FirstOrDefaultAsync(school => school.Id ==schoolUpdate.Id);
                    if (data != null)
                    {
                        _mapper.Map(schoolUpdate, data);

                        await _dbContext.SaveChangesAsync();

                        responseModel.Data = true;
                        responseModel.Status = 200; 
                    }
                    else
                    {
                        // If the record with the given ID is not found
                        responseModel.Data = false;
                        responseModel.Status = 404; // Not Found
                    }
                }
                else
                {
                    // If the ID is less than or equal to zero
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

    }
}
