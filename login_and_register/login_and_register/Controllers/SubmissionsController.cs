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
        public async Task<IActionResult> AddSubmission(SubmissionModel submission) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(e=>e.Email== submission.Email);
            if(user == null) 
                return NotFound("User is not found");


            foreach (var qsub in submission.Questionssub)
            {
                var sub = new Submission
                {
                    ApplicationUserId = user.Id,
                    ExamId = submission.ExamId,
                    Grade = submission.Grade,
                    QuestionId = qsub.QuestionId,
                    StudentAnswer = qsub.StudentAnswer
                };

                await _context.Submissions.AddAsync(sub);
            }

           
            await _context.SaveChangesAsync();

            return Ok(submission);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSubmission(int id) 
        {
            var submission = await _context.Submissions.FindAsync(id);

            if (submission == null)
                return NotFound("Exam is not found");
                      
            return Ok (submission);
        }
    }
}
