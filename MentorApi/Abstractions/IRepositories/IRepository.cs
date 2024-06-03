using MentorApi.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MentorApi.Abstractions.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        //niye bezileri aysnc bezileri yox?
        DbSet<T> Table {  get; }
        IQueryable<T> GetAll();
        Task<T> GetByIDAsync(int id);
        Task<bool> AddAsync(T data);
        bool Remove(T data);
        Task<bool> RemoveByID(int id);
        bool Update(T data);
    }
}
