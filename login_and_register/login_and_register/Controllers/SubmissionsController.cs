using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubmission(SubmissionModel submissionModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == submissionModel.Email);
            if (user == null)
                return NotFound("User is not found");

            if (await _context.Submissions.Where(e => e.ExamId == submissionModel.ExamId).AnyAsync(e => e.ApplicationUserId == user.Id))
                return BadRequest("User has already submitted the exam");

            var sub = new Submission
            {
                ApplicationUserId = user.Id,
                ExamId = submissionModel.ExamId,
                Grade = submissionModel.Grade
            };

            await _context.Submissions.AddAsync(sub);
            await _context.SaveChangesAsync();

            foreach (var questionSub in submissionModel.Questionssub)
            {
                var questionSubmission = new QuestionsSubs
                {
                    SubmissionId = sub.Id,
                    QuestionId = questionSub.QuestionId,
                    StudentAnswer = questionSub.StudentAnswer
                };

                await _context.QuestionsSubs.AddAsync(questionSubmission);

            }

            _context.SaveChanges();

            return Ok(submissionModel);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmission(int id)
        {
            var submission = await _context.Submissions.Where(e => e.ExamId == id).FirstOrDefaultAsync();
            var quest = await _context.QuestionsSubs.Where(w => w.SubmissionId == submission.Id).Select(e => new {e.QuestionId,e.StudentAnswer }).ToListAsync();

            if (submission == null)
                return NotFound("Exam is not found");

            return Ok(new { submission, quest });
        }
    }
}
