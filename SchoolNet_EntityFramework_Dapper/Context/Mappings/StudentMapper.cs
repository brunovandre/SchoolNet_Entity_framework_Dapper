using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_EntityFramework_Dapper.Entities;

namespace SchoolNet_EntityFramework_Dapper.Context.Mappings
{
    public class StudentMapper : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.Property(e => e.Id).HasColumnName("StudentId");

            builder.HasMany(c => c.StudentClasses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);
        }
    }
}
