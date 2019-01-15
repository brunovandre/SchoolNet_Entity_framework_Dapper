using Microsoft.EntityFrameworkCore;
using SchoolNet_EntityFramework.Context.Mappings;
using SchoolNet_EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework.Context
{
    public class SchoolNetContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }

        public SchoolNetContext(DbContextOptions<SchoolNetContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentMapper());
            modelBuilder.ApplyConfiguration(new CourseMapper());
            modelBuilder.ApplyConfiguration(new TeacherMapper());
            modelBuilder.ApplyConfiguration(new StudentClassMapper());
        }
    }
}
