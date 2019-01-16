namespace SchoolNet_EntityFramework_Dapper.Entities
{
    public class StudentClass
    {
        public virtual int Id { get; set; }

        private int StudentClassId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    }
}
