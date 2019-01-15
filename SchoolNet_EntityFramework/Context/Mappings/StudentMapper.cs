using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_EntityFramework.Entities;

namespace SchoolNet_EntityFramework.Context.Mappings
{
    public class StudentMapper : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasMany(c => c.StudentClasses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);
        }
    }
}
