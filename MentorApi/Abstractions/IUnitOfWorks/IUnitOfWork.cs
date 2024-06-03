using MentorApi.Abstractions.IRepositories;
using MentorApi.Abstractions.IRepositories.ISchoolRepos;
using MentorApi.Abstractions.IRepositories.IStudentRepos;
using MentorApi.Entities.Common;

namespace MentorApi.Abstractions.IUnitOfWorks
{
    public interface IUnitOfWork:IDisposable
    {
        //ISchoolRepository Schools {  get; }
        //IStudentRepository Students { get; }
        //void Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();

    }
}
