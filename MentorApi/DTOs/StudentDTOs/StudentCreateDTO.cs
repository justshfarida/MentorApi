using MentorApi.Entities.AppdbContextEntity;

namespace MentorApi.DTOs.StudentDTOs
{
    public class StudentCreateDTO
    {
        public string Name { get; set; }
        public int SchoolID { get; set; }
    }
}
