using MentorApi.Entities.AppdbContextEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorApi.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.SchoolID).IsRequired();
        }
    }
}
