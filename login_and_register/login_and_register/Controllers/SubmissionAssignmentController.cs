using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionAssignmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private List<string> _allowedExtensions = new List<string> { ".pdf", ".doc", ".docx", ".png", ".jpg" };

        public SubmissionAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddAssignmentSubmission")]
        public async Task<IActionResult> AddSubmission([FromForm]SubAssModel submission)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == submission.UserEmail);
            if (user == null)
                return NotFound("User is not found");

            if (submission == null || !ModelState.IsValid || user is null)
                return NotFound("Model is not found");

            var datastream = new MemoryStream();

            if (submission.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(submission.File.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");
                await submission.File.CopyToAsync(datastream);
            }

            var SubAss = new SubmissionAssignment
            {
                ApplicationUserId = user.Id,
                AssignmentId = submission.AssignmentId,
                Grade = submission.Grade,
                File = submission.File is null? null: datastream.ToArray(),
            };

            await _context.SubmissionAssignments.AddAsync(SubAss);
            _context.SaveChanges();

            return Ok(SubAss);
        }


        [HttpGet("GetAssignmentSubmission/{id}")]
        public async Task<IActionResult> GetAssignmentSubmission(int id)
        {

            var SubmissionA = await _context.SubmissionAssignments.FindAsync(id);

            if (SubmissionA == null)
                return NotFound("Submission is not found");

            return Ok(SubmissionA);

        }
    }
}
