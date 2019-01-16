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
using Slapper;

namespace SchoolNet_EntityFramework_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public TeachersController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Database.GetDbConnection().QueryAsync<dynamic>(@"
                SELECT T.TeacherId AS Id,
                       T.FirstName,
                       T.LastName,
                       SC.StudentClassId AS StudentClasses_Id,
                       SC.CourseId AS StudentClasses_CourseId,
                       SC.TeacherId AS StudentClasses_TeacherId,
                       SC.StudentId AS StudentClasses_StudentId
                FROM   Teacher as T INNER JOIN StudentClass as SC
                ON     SC.TeacherId = T.TeacherId");

            AutoMapper.Configuration.AddIdentifier(typeof(Teacher), "Id");
            AutoMapper.Configuration.AddIdentifier(typeof(StudentClass), "Id");

            var teachers = (AutoMapper.MapDynamic<Teacher>(data)
                as IEnumerable<Teacher>).ToList();

            return Ok(teachers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<dynamic>(@"
                SELECT T.TeacherId AS Id,
                       T.FirstName,
                       T.LastName,
                       SC.StudentClassId AS StudentClasses_Id,
                       SC.CourseId AS StudentClasses_CourseId,
                       SC.TeacherId AS StudentClasses_TeacherId,
                       SC.StudentId AS StudentClasses_StudentId
                FROM   Teacher as T INNER JOIN StudentClass as SC
                ON     SC.TeacherId = T.TeacherId");

            AutoMapper.Configuration.AddIdentifier(typeof(Teacher), "Id");
            AutoMapper.Configuration.AddIdentifier(typeof(StudentClass), "Id");

            var teacher = (AutoMapper.MapDynamic<Teacher>(data) as Teacher);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);

            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] Teacher teacher, int id)
        {
            if (!await _context.Teachers.AnyAsync(c => c.Id == id)) return NotFound();

            _context.Entry(teacher).State = EntityState.Modified;

            _context.Teachers.Update(teacher);

            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await _context.Teachers.AnyAsync(c => c.Id == id)) return NotFound();

            var entity = new Teacher() { Id = id };
            _context.Teachers.Attach(entity);

            _context.Teachers.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}