using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private List<string> _allowedExtensions = new List<string> { ".pdf", ".doc", ".png", ".jpg", ".jpeg" };

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromForm] CourseModel course)
        {
           
            if (course == null || !ModelState.IsValid)
                return BadRequest("Bad Request");

            using var datastream = new MemoryStream();
            if (course.photo != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(course.photo.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");
                await course.photo.CopyToAsync(datastream);
            }
            var newcourse = new Course
            {

                CourseName = course.Name,
                Description = course.Description,
                File = course.photo is null? null:datastream.ToArray(),
            };

            await _context.Courses.AddAsync(newcourse);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(e=>e.Email==course.Email);
            var crse = await _context.Courses.FirstOrDefaultAsync(e=>e.CourseName == course.Name);
            System.Threading.Thread.Sleep(5000);
            var enroll = new UserCourse { ApplicationUserId = user.Id, CourseId = crse.Id };
            await _context.UserCourses.AddAsync(enroll);
            await _context.SaveChangesAsync();
            return Ok(newcourse);
        }

      


        [HttpGet("GetUserCourses")]
        public async Task<IActionResult> GetUserCourses([FromHeader] string usermail)
        {
            if (!await _context.Users.AnyAsync(c => c.Email == usermail))
                return BadRequest("Id is not valid");

            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == usermail);
            var usercourse = await _context.UserCourses.Include(e => e.Course).Where(e => e.ApplicationUserId == user.Id).ToListAsync();
            //var courses = await _context.Courses.Where(e => e.Id == usercourse.CourseId).Include(e=>e.UserCourses).ToListAsync();

            if (user == null || usercourse is null)
                return NotFound("Lectures are not found");

            var list = new { user, usercourse };

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            if (!await _context.Courses.AnyAsync(x => x.Id == id))
                return BadRequest("Id is not valid");

            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound("Course is not found");

            return Ok(course);
        }

        [HttpGet("GetCoursePlaylist{courseId}")]
        public async Task<IActionResult> GetCoursePlaylist(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
                return NotFound("Corse is not found");

            return Ok(course.Playlist);
        }


        [HttpPut("AddPlayList")]
        public async Task<IActionResult> AddPlayList([FromBody] CoursePlaylistModel playlist) 
        {
            var course = await _context.Courses.FindAsync(playlist.CourseId);

            if (course == null) { return BadRequest("Corse is not found"); }

            course.Playlist = playlist.Playlist;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();


            return Ok(course);
        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromForm] CourseModel course)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not found");

            var coursetoupdate = await _context.Courses.FindAsync(id);

            if (coursetoupdate == null)
                return NotFound("Course is not found");

            coursetoupdate.CourseName = course.Name;
            coursetoupdate.Description = course.Description;
            if (course.photo != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(course.photo.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");


                using var dataStraem = new MemoryStream();

                await course.photo.CopyToAsync(dataStraem);

                coursetoupdate.File = dataStraem.ToArray();
            }

            _context.Courses.Update(coursetoupdate);
            await _context.SaveChangesAsync();

            return Ok(coursetoupdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {

            var courseyodelete = await _context.Courses.FindAsync(id);

            if (courseyodelete == null)
                return NotFound("Corse is npt found");

            _context.Courses.Remove(courseyodelete);
            await _context.SaveChangesAsync();

            return Ok(courseyodelete);
        }

    }
}
