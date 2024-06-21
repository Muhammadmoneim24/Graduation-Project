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
            };

            await _context.Submissions.AddAsync(sub);
            await _context.SaveChangesAsync();

            int totalGrade = 0;
            bool allQuestionsHaveCorrectAnswers = true;

            var separator = new char[] { '/', ',' };

            foreach (var questionSub in submissionModel.Questionssub)
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionSub.QuestionId);
                if (question == null)
                    continue;

                var questionSubmission = new QuestionsSubs
                {
                    SubmissionId = sub.Id,
                    QuestionId = questionSub.QuestionId,
                    StudentAnswer = questionSub.StudentAnswer,
                };

                await _context.QuestionsSubs.AddAsync(questionSubmission);
                await _context.SaveChangesAsync();

                if (string.IsNullOrEmpty(question.CorrectAnswer))
                {
                    allQuestionsHaveCorrectAnswers = false;
                }
                else
                {
                    var correctAnswers = question.CorrectAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var studentAnswers = questionSub.StudentAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (correctAnswers.Count == studentAnswers.Count && !correctAnswers.Except(studentAnswers).Any())
                    {
                        totalGrade += question.Points;
                    }
                }

                submissionModel.Grade = totalGrade;
            }

            if (!allQuestionsHaveCorrectAnswers)
            {
                return Ok("Submission saved, wait for result.");
            }

            sub.Grade = totalGrade;
            _context.Submissions.Update(sub);
            await _context.SaveChangesAsync();

            return Ok(submissionModel);
        }





        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmission(int id)
        {
            var submission = await _context.Submissions.Where(e => e.ExamId == id).FirstOrDefaultAsync();
            var quest = await _context.QuestionsSubs.Where(w => w.SubmissionId == submission.Id).Select(e => new { e.QuestionId, e.StudentAnswer }).ToListAsync();

            if (submission == null)
                return NotFound("Exam is not found");

            return Ok(new { submission, quest });
        }

        [HttpPut("UpdateExamGrade")]
        public async Task<IActionResult> UpdateExamGrade(int examid, [FromBody] UpdateexamgradeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad Request");

            var exam = await _context.Exams.FindAsync(examid);
            var student = await _context.Users.FindAsync(model.StudentId);
            var sub = await _context.Submissions.Where(e=>e.ExamId == examid).FirstOrDefaultAsync(e=>e.ApplicationUserId == model.StudentId);


            if (sub == null || exam == null)
                return NotFound("Model is not found");

            sub.Grade = model.Grade;

            _context.SaveChanges();

            return Ok(sub);

        }
    }
}
