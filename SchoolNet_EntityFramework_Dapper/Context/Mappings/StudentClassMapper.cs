using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_EntityFramework_Dapper.Entities;

namespace SchoolNet_EntityFramework_Dapper.Context.Mappings
{
    public class StudentClassMapper : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            builder.ToTable("StudentClass");

            builder.Property(e => e.Id).HasColumnName("StudentClassId");

            builder.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.CourseId);

            builder.HasOne(sc => sc.Student)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.StudentId);

            builder.HasOne(sc => sc.Teacher)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.TeacherId);
        }
    }
}
