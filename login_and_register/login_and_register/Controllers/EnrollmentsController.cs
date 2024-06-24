using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddEnrollment(EnrollmentModel enrollment)
        {
            var course = await _context.Courses.FindAsync(enrollment.CourseId);
            var std = await _context.Users.FirstOrDefaultAsync(e => e.Email == enrollment.Email);

            if (course is null || std is null)
                return BadRequest("User or course not found");

            if(await _context.UserCourses.Where(e => e.CourseId == enrollment.CourseId).AnyAsync(e => e.ApplicationUserId == std.Id))
                return BadRequest("User has already enrolled in this course");


            var enroll = new UserCourse {

                ApplicationUserId = std.Id,
                CourseId = course.Id,

            };

            if(enroll is null) 
            {
                return BadRequest("Bad Request");
            }

            await _context.UserCourses.AddAsync(enroll);
            _context.SaveChanges();

            return Ok(enroll);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id,EnrollmentModel enrollment)
        {
            var course = await _context.Courses.FindAsync(enrollment.CourseId);
            var std = await _context.Users.FirstOrDefaultAsync(e => e.Email == enrollment.Email);

            if (course is null || std is null)
                return BadRequest("User or course not found");

            var enroll = await _context.UserCourses.FindAsync(id);
            if (enroll is null)
                return NotFound("User Courses not found");

            enroll.ApplicationUserId = std.Id;
            enroll.CourseId = course.Id;

            _context.UserCourses.Update(enroll);
            await _context.SaveChangesAsync();

            return Ok(enroll);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrolls( int id) 
        {
            var enroll = await _context.UserCourses.FirstOrDefaultAsync(e=>e.CourseId == id);

            var stds = await _context.Users.Where(e=>e.Id == enroll.ApplicationUserId).ToListAsync();

            return Ok(stds);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteeEnrollment(int id, EnrollmentModel enrollment)
        {
            var course = await _context.Courses.FindAsync(enrollment.CourseId);
            var std = await _context.Users.FirstOrDefaultAsync(e => e.Email == enrollment.Email);

            if (course is null || std is null)
                return BadRequest("User or course not found");

            var enroll = await _context.UserCourses.FindAsync(id);
            if (enroll is null)
                return NotFound("User Courses not found");

            _context.UserCourses.Remove(enroll);
            _context.SaveChanges();

            return Ok(enroll);

        }




    }
}
