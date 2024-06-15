using MentorApi.Entities.Common;

namespace MentorApi.Entities.AppdbContextEntity
{
    public class School:BaseEntity
    {
        public int Number {  get; set; }
        public string Name { get; set; }
        
        public IList<Student> Students { get; set; }

    }
}
    