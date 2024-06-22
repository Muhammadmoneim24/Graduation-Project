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
                    questionSubmission.AnswerPoints = 0;
                    _context.QuestionsSubs.Update(questionSubmission);
                }
                else
                {
                    var correctAnswers = question.CorrectAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var studentAnswers = questionSub.StudentAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (correctAnswers.Count == studentAnswers.Count && !correctAnswers.Except(studentAnswers).Any())
                    {
                        questionSubmission.AnswerPoints = question.Points;
                        totalGrade += question.Points;
                    }
                    else
                    {
                        questionSubmission.AnswerPoints = 0;
                    }

                    _context.QuestionsSubs.Update(questionSubmission);
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



        [HttpPut("manual-correction")]
        public async Task<IActionResult> ManualCorrection(List<ManualModel> corrections)
        {
            if (corrections == null || !corrections.Any())
            {
                return BadRequest("No corrections provided");
            }

            int totalGrade = 0;
            int? submissionId = null;

            foreach (var correction in corrections)
            {
                var questionSubmission = await _context.QuestionsSubs.FirstOrDefaultAsync(qs => qs.Id == correction.QuestionsSubId);

                if (questionSubmission == null)
                {
                    return NotFound($"Question submission with ID {correction.QuestionsSubId} not found");
                }

                questionSubmission.AnswerPoints = correction.AnswerPoints;
                totalGrade += correction.AnswerPoints;

                _context.QuestionsSubs.Update(questionSubmission);


                submissionId = questionSubmission.SubmissionId;

            }


            var submission = await _context.Submissions.FirstOrDefaultAsync(s => s.Id == submissionId.Value);

            if (submission == null)
            {
                return NotFound("Submission not found");
            }

            submission.Grade = totalGrade;
            _context.Submissions.Update(submission);
            await _context.SaveChangesAsync();

            return Ok(new { Grade = totalGrade });



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
    }
}
