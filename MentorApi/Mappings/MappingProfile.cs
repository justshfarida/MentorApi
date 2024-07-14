using AutoMapper;
using MentorApi.DTOs.SchoolDTOs;
using MentorApi.DTOs.StudentDTOs;
using MentorApi.DTOs.UserDTOs;
using MentorApi.Entities.AppdbContextEntity;
using MentorApi.Entities.Identity;
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
            CreateMap<AppUser, CreateUserDTO>().ReverseMap();
            CreateMap<AppUser, UserGetDTO>().ReverseMap();
            CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
        } 
    }
}
