using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework_Dapper.Entities
{
    public class Teacher
    {
        public virtual int Id { get; set; }

        private int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
