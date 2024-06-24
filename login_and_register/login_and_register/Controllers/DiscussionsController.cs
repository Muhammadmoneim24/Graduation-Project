using login_and_register.Dtos;
using login_and_register.Models;
using login_and_register.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        private List<string> _allowedExtensions = new List<string> { ".pdf", ".doc", ".png", ".jpg" };

        public DiscussionsController(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateDiscussion(int id, [FromForm] DiscussionModel discussion)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == discussion.UserEmail);

            if (discussion == null || !ModelState.IsValid)
                return NotFound("Model is not found");

            var datastream = new MemoryStream();
            if (discussion.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(discussion.File.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");
                await discussion.File.CopyToAsync(datastream);
            }
            var disc = new Discussion
            {
                CourseId = id,
                Tittle = discussion.Tittle,
                Content = discussion.Content,
                File = discussion.File is null ? null : datastream.ToArray(),
            };

            await _context.Discussions.AddAsync(disc);
            _context.SaveChanges();

            var enrolls = await _context.UserCourses.Where(uc => uc.CourseId == id).ToListAsync();
            foreach (var enroll in enrolls)
            {
                await _notificationService.SendNotificationAsync(enroll.ApplicationUserId, "New Discussion", $"A new discussion '{discussion.Tittle}' has been posted in course {id}.");
            }

            return Ok(disc);
        }
        [HttpGet("GetCoursePosts{id}")]
        public async Task<IActionResult> GetCourseLectures(int id)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var discussions = await _context.Discussions.Where(e => e.CourseId == id).ToListAsync();


            if (discussions == null)
                return NotFound("Lectures are not found");

            return Ok(discussions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscussion(int id)
        {
            if (!await _context.Discussions.AnyAsync(x => x.Id == id))
                return BadRequest("Id is not valid");

            var disc = await _context.Discussions.FindAsync(id);

            if (disc == null)
                return NotFound("Lec is not found");

            return Ok(disc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromForm] DiscussionModel discussion)
        {
            var disc = await _context.Discussions.FindAsync(id);

            if (discussion == null || disc == null)
                return NotFound("Model is not found");

            if (disc.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(discussion.File.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");


                using var dataStraem = new MemoryStream();

                await discussion.File.CopyToAsync(dataStraem);

                disc.File = dataStraem.ToArray();
            }

            disc.Tittle = discussion.Tittle;
            disc.Content = discussion.Content;

              _context.Discussions.Update(disc);
            await _context.SaveChangesAsync();

            return Ok(disc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            if (!await _context.Discussions.AnyAsync(q => q.Id == id))
                return NotFound("id is not found");

            var disc = await _context.Discussions.FindAsync(id);
            if (disc == null)
                return NotFound("Model is not found");

            _context.Discussions.Remove(disc);
            _context.SaveChanges();

            return Ok(disc);
        }
    }
}
