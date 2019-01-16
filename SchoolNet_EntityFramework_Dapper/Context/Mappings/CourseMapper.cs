using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_EntityFramework_Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework_Dapper.Context.Mappings
{
    public class CourseMapper : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");

            builder.Property(e => e.Id).HasColumnName("CourseId");

            builder.HasMany(c => c.StudentClasses)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
