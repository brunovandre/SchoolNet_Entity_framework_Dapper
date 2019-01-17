using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolNet_EntityFramework_Dapper.Context;
using SchoolNet_EntityFramework_Dapper.Entities;

namespace SchoolNet_EntityFramework_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentClassController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public StudentClassController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var studentClasses = await _context.Database.GetDbConnection().QueryAsync<StudentClass, Course, Student, Teacher, StudentClass>(@"
               SELECT SC.StudentClassId AS Id,
                      SC.*,
                      C.*,  
                      S.*,
                      T.*
               FROM StudentClass as SC INNER JOIN Course as C
               ON SC.CourseId = C.CourseId
               INNER JOIN Student as S
               ON SC.StudentId = S.StudentId
               INNER JOIN Teacher as T
               ON SC.TeacherId = T.TeacherId",
               map: (sc, c, s, t) =>
               {                   
                   sc.Course = c;
                   sc.Course.Id = sc.CourseId;
                   sc.Student = s;
                   sc.Student.Id = sc.StudentId;
                   sc.Teacher = t;
                   sc.Teacher.Id = sc.TeacherId;

                   return sc;
               },
               splitOn: "CourseId, StudentId, TeacherId"
            );

            return Ok(studentClasses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var studentClass = await _context.Database.GetDbConnection().QueryAsync<StudentClass, Course, Student, Teacher, StudentClass>(@"
               SELECT SC.StudentClassId AS Id,
                      SC.*,
                      C.*,  
                      S.*,
                      T.*
               FROM StudentClass as SC INNER JOIN Course as C
               ON SC.CourseId = C.CourseId
               INNER JOIN Student as S
               ON SC.StudentId = S.StudentId
               INNER JOIN Teacher as T
               ON SC.TeacherId = T.TeacherId
               WHERE SC.StudentClassId = @id",
               map: (sc, c, s, t) =>
               {
                   sc.Course = c;
                   sc.Course.Id = sc.CourseId;
                   sc.Student = s;
                   sc.Student.Id = sc.StudentId;
                   sc.Teacher = t;
                   sc.Teacher.Id = sc.TeacherId;

                   return sc;
               },
               splitOn: "CourseId, StudentId, TeacherId",
               param: new { id }
            );

            if (studentClass == null || !studentClass.Any())
                return NotFound();

            return Ok(studentClass.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentClass studentClass)
        {
            await _context.StudentClasses.AddAsync(studentClass);

            await _context.SaveChangesAsync();

            return Ok(studentClass);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var entityId = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(@"
                SELECT StudentClassId from StudentClass
                WHERE StudentClassId = @id",
                new { id });

            if (entityId == default(int)) return NotFound();

            var entity = new StudentClass() { Id = id };
            _context.StudentClasses.Attach(entity);

            _context.StudentClasses.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}