using MentorApi.Abstractions.IRepositories.IStudentRepos;
using MentorApi.Context;
using MentorApi.Entities.AppdbContextEntity;

namespace MentorApi.Implementations.Repositories.EntitiesRepos
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolDbContext schoolDbContext) : base(schoolDbContext)
        {
        }
    }
}
