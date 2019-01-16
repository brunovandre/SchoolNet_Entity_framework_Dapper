using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework_Dapper.Entities
{
    public class Course
    {
        public virtual int Id { get; set; }

        private int CourseId { get; set; }        
        public string Name { get; set; }
        public int WorkLoad { get; set; }
        public decimal Price { get; set; }
        public bool Online { get; set; }

        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
