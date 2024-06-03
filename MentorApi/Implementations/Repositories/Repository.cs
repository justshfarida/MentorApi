using MentorApi.Abstractions.IRepositories;
using MentorApi.Context;
using MentorApi.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MentorApi.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SchoolDbContext _context;
        public  Repository(SchoolDbContext schoolDbContext)
        {
            _context= schoolDbContext;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T data)
        {
            EntityEntry<T> entityEntry=await Table.AddAsync(data);
            return entityEntry.State == EntityState.Added;
        }

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await Table.AsQueryable().FirstOrDefaultAsync(data=>data.Id==id);

        }

        public bool Remove(T data)
        {
            EntityEntry<T> entityEntry = Table.Remove(data);

            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByID(int id)
        {
            T entity = await Table.FindAsync(id);
            if (entity == null)
                return false; // Entity not found
            EntityEntry<T> entityEntry=Table.Remove(entity);
            return entityEntry.State==EntityState.Deleted; 
        }

        public bool Update(T data)
        {
            EntityEntry<T> entityEntry=Table.Update(data); 
            return entityEntry.State == EntityState.Modified;
        }
    }
}
