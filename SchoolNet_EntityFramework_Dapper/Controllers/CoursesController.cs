using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolNet_EntityFramework_Dapper.Context;
using SchoolNet_EntityFramework_Dapper.Entities;
using System.Threading.Tasks;

namespace SchoolNet_EntityFramework_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolNetContext _context;

        public CoursesController(SchoolNetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Database.GetDbConnection().QueryAsync<Course>(@"
                SELECT Course.CourseId as Id,
                       Course.*
                FROM   Course");

            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Course>(@"
                SELECT Course.CourseId as Id,
                       Course.*
                FROM   Course
                WHERE CourseId = @id",
                new { id});

            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            await _context.Courses.AddAsync(course);

            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] Course course, int id)
        {
            var entityId = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(@"
                SELECT Id from Course
                WHERE Id = @id",
                new { id});

            if (entityId == default(int)) return NotFound();

            _context.Entry(course).State = EntityState.Modified;

            _context.Courses.Update(course);

            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var entityId = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(@"
                SELECT Id from Course
                WHERE Id = @id",
                new { id });

            if (entityId == default(int)) return NotFound();

            var entity = new Course() { Id = id };
            _context.Courses.Attach(entity);

            _context.Courses.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}