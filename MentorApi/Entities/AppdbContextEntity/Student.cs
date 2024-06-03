using MentorApi.Entities.Common;

namespace MentorApi.Entities.AppdbContextEntity
{
    public class Student:BaseEntity
    {
        public string Name { get; set; }
        public School School { get; set; }
        public int SchoolID { get; set; }
    }
}
