using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework_Dapper.Entities
{
    public class Student
    {
        public virtual int Id { get; set; }

        private int StudentId { get; set; }        
        public string FullName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<StudentClass> StudentClasses { get; set; }

        public Student()
        {
            StudentClasses = new HashSet<StudentClass>();
        }
    }
}
