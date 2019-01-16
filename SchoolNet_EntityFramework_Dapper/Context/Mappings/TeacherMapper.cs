using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_EntityFramework_Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework_Dapper.Context.Mappings
{
    public class TeacherMapper : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teacher");

            builder.Property(e => e.Id).HasColumnName("TeacherId");

            builder.HasMany(c => c.StudentClasses)
                .WithOne(sc => sc.Teacher)
                .HasForeignKey(sc => sc.TeacherId);
        }
    }
}
