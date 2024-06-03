using MentorApi.Entities.AppdbContextEntity;
using Microsoft.EntityFrameworkCore;

namespace MentorApi.Context
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<School>  Schools { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }
}
