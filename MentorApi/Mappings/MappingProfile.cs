using AutoMapper;
using MentorApi.DTOs.SchoolDTOs;
using MentorApi.DTOs.StudentDTOs;
using MentorApi.Entities.AppdbContextEntity;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MentorApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentGetDTO>()
                    .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                    .ReverseMap();

            CreateMap<Student, StudentCreateDTO>().ReverseMap();
            CreateMap<Student, StudentUpdateDTO>().ReverseMap();
            CreateMap<Student, ChangeSchoolDTO>().ReverseMap();
            CreateMap<School, SchoolCreateDTO>().ReverseMap();
            CreateMap<School, SchoolGetDTO>().ReverseMap();
            CreateMap<School, SchoolUpdateDTO>().ReverseMap();
        }
    }
}
