using Microsoft.EntityFrameworkCore;
using SchoolNet_EntityFramework_Dapper.Context.Mappings;
using SchoolNet_EntityFramework_Dapper.Entities;

namespace SchoolNet_EntityFramework_Dapper.Context
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
