using MentorApi.Entities.AppdbContextEntity;

namespace MentorApi.DTOs.StudentDTOs
{
    public class StudentGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolName { get; set; }
    }
}
