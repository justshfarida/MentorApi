using MentorApi.Abstractions.IRepositories.ISchoolRepos;
using MentorApi.Context;
using MentorApi.Entities.AppdbContextEntity;

namespace MentorApi.Implementations.Repositories.EntitiesRepos
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(SchoolDbContext schoolDbContext) : base(schoolDbContext)
        {
        }
    }
}
