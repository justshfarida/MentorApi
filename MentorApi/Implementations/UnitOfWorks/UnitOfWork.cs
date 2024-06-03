using MentorApi.Abstractions.IRepositories;
using MentorApi.Abstractions.IRepositories.ISchoolRepos;
using MentorApi.Abstractions.IRepositories.IStudentRepos;
using MentorApi.Abstractions.IUnitOfWorks;
using MentorApi.Context;
using MentorApi.Entities.Common;
using MentorApi.Implementations.Repositories;
using MentorApi.Implementations.Repositories.EntitiesRepos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MentorApi.Implementations.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext DbContext;
        private Dictionary<Type, object> _repositories;
        //public ISchoolRepository Schools => new SchoolRepository(DbContext);

        //public IStudentRepository Students =>new StudentRepository(DbContext);
        public UnitOfWork(SchoolDbContext dbContext) 
        { 
            DbContext = dbContext;
            _repositories =new();


        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            IRepository<TEntity> repository=new Repository<TEntity>(DbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
