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
        public async Task<IActionResult> AddSubmission([FromForm] SubAssModel submission)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == submission.UserEmail);
            if (user == null)
                return NotFound("User is not found");

            if (submission == null || !ModelState.IsValid)
                return BadRequest("Invalid model");


            if (await _context.SubmissionAssignments
                .FirstOrDefaultAsync(sa => sa.ApplicationUserId == user.Id && sa.AssignmentId == submission.AssignmentId) != null)
            {
                return Conflict("A submission for this user and assignment already exists.");
            }

            var datastream = new MemoryStream();
            if (submission.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(submission.File.FileName).ToLower()))
                    return Conflict("File extension is not allowed");

                await submission.File.CopyToAsync(datastream);
            }

            var SubAss = new SubmissionAssignment
            {
                ApplicationUserId = user.Id,
                AssignmentId = submission.AssignmentId,
                Grade = 0,
                File = submission.File == null ? null : datastream.ToArray(),
            };

            await _context.SubmissionAssignments.AddAsync(SubAss);
            await _context.SaveChangesAsync();

            return Ok(SubAss);
        }

        [HttpPut("AdddAssignmentGrade")]
        public async Task<IActionResult> AdddAssignmentGrade([FromForm] StudentAssModel submission)
        {
            if (submission == null || !ModelState.IsValid)
                return NotFound("Model is not found");
            
            var user = await _context.Users.FindAsync(submission.UserId);
            if (user == null)
                return NotFound("User is not found");
            
            var assign = await _context.SubmissionAssignments.Where(sa => sa.AssignmentId == submission.AssignmentId && sa.ApplicationUserId == user.Id).FirstOrDefaultAsync();

            if (assign == null)
                return NotFound("Assignment submission is not found");

            assign.Grade = submission.Grade;

            _context.SubmissionAssignments.Update(assign);
            await _context.SaveChangesAsync();

            return Ok(assign);
        }

        [HttpGet("GetSubmission{Subid}")]
        public async Task<IActionResult> GetSubmission(int Subid)
        {
            var SubmissionA = await _context.SubmissionAssignments.FindAsync(Subid);

            if (SubmissionA == null)
                return NotFound("Submission is not found");

            return Ok(SubmissionA);
        }

        [HttpGet("GetAssignmentSubmissions{Assignmentid}")]
        public async Task<IActionResult> GetAssignmentSubmissions(int Assignmentid)
        {
            var SubmissionA = await _context.SubmissionAssignments.Include(e=>e.ApplicationUser).Where(e => e.AssignmentId == Assignmentid).ToListAsync();

            if (SubmissionA == null )
                return NotFound("Submission is not found");

            return Ok(SubmissionA);
        }

        [HttpGet("GetAllSubmissions")]
        public async Task<IActionResult> GetAllSubmissions()
        {
            var Submissions = await _context.SubmissionAssignments.ToListAsync();

            if (Submissions == null)
                return NotFound("Submission is not found");

            return Ok(Submissions);
        }
    }
}
