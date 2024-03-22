using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateCourse")]

        public async Task<IActionResult> CreateCourse(CourseModel course)
        {
            if (course == null || !ModelState.IsValid)
                return BadRequest("Bad Request");

            var newcourse = new Course { 
            
                CourseName = course.Name,
                Description = course.Description,
                Link = course.Link,
            };

            await _context.Courses.AddAsync(newcourse);
            await _context.SaveChangesAsync();
            return Ok(newcourse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id) 
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound("Corse is not found");

            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id , CourseModel course) 
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not found");

            var coursetoupdate = await _context.Courses.FindAsync (id);

            if (coursetoupdate == null)
                return NotFound("Course is not found");

            coursetoupdate.CourseName = course.Name;
            coursetoupdate.Description = course.Description;
            coursetoupdate.Link = course.Link;

            _context.SaveChanges();

            return Ok(coursetoupdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id) 
        {
            if(!await _context.Courses.AllAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var courseyodelete = await _context.Courses.FindAsync(id);

            if (courseyodelete == null)
                return NotFound("Corse is npt found");

            _context.Courses.Remove(courseyodelete);
            _context.SaveChanges();

            return Ok(courseyodelete);
        }

    }
}
