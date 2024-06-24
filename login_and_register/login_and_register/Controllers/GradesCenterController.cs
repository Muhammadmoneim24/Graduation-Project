using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesCenterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradesCenterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetExaamGrades/{id}")]
        public async Task<IActionResult> GetExaamGrades(int id)
        {
            if (!await _context.Submissions.AnyAsync(e => e.ExamId == id))
                return BadRequest("Exam's grades are not Found");

            var studentGrades = await _context.Submissions.Where(ex => ex.ExamId == id).Select(sub => new
            {
                sub.ApplicationUser,
                sub.Grade
            }).ToListAsync();

            if (studentGrades == null || !studentGrades.Any())
            {
                return NotFound("No submissions found for this exam");
            }

            return Ok(studentGrades);
        }

        [HttpGet("GetStudentExam/{examid}/{studentid}")]
        public async Task<IActionResult> GetStudentExam(int examid, string studentid)
        {
            var exam = await _context.Exams.FindAsync(examid);
            var student = await _context.Users.FindAsync(studentid);
            var sub = await _context.Submissions.FirstOrDefaultAsync(e => e.ExamId == examid && e.ApplicationUserId == studentid);

            if (exam == null || student == null)
                return NotFound("Exam or student is not found");

            var separator = new char[] { '/', ',' };
            var questions = await _context.Questions
                .Where(e => e.ExamId == examid)
                .Select(e => new {
                    e.Id,
                    e.ExamId,
                    e.Type,
                    e.Text,
                    Options = e.Options.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    CorrectAnswer = e.CorrectAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    e.Points,
                    e.Explanation
                })
                .ToListAsync();

            var questionIds = questions.Select(q => q.Id).ToList();

            var studentAnswers = await _context.QuestionsSubs
                .Where(s => s.SubmissionId == sub.Id && questionIds.Contains(s.QuestionId))
                .ToListAsync();

            var answerPoints = await _context.QuestionsSubs
                .Where(qs => questionIds.Contains(qs.QuestionId) && qs.SubmissionId == sub.Id)
                .GroupBy(qs => qs.QuestionId)
                .Select(g => new { QuestionId = g.Key, AnswerPoints = g.First().AnswerPoints })
                .ToDictionaryAsync(q => q.QuestionId, q => q.AnswerPoints);

            var examWithStudentAnswers = new
            {
                exam = new { exam.Id, exam.CourseId, exam.Tittle, exam.Describtion, exam.Date, exam.Time, sub.Grade, exam.Instructions, exam.NumOfQuestions },
                Questions = questions.Select(q => new
                {
                    q.Id,
                    q.ExamId,
                    q.Type,
                    q.Text,
                    q.Options,
                    CorrectAnswr = q.CorrectAnswer.OrderByDescending(o =>
                    {
                        int intValue;
                        return int.TryParse(o, out intValue) ? intValue : int.MinValue;
                    }).ToList(),
                    q.Points,
                    answerpoints = answerPoints.ContainsKey(q.Id) ? answerPoints[q.Id] : 0,
                    q.Explanation,
                    StudentAnswer = studentAnswers
                        .Where(sa => sa.QuestionId == q.Id)
                        .SelectMany(e => e.StudentAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                        .OrderByDescending(o =>
                        {
                            int intValue;
                            return int.TryParse(o, out intValue) ? intValue : int.MinValue;
                        })
                        .ToList()
                }).ToList()
            };

            return Ok(examWithStudentAnswers);
        }



        [HttpGet("GetAssignmentGrades/{id}")]
        public async Task<IActionResult> GetAssignmentGrades(int id)
        {
            if (!await _context.SubmissionAssignments.AnyAsync(e => e.AssignmentId == id))
                return BadRequest("Assignment's grades are not Found");

            var AssstdGrades = await _context.SubmissionAssignments.Where(ex => ex.AssignmentId == id).Select(sub => new
            {
                sub.ApplicationUser,
                sub.Grade
            }).ToListAsync();

            if (AssstdGrades == null || !AssstdGrades.Any())
            {
                return NotFound("No submissions found for this exam");
            }

            return Ok(AssstdGrades);
        }

        [HttpGet("GetStudentAssignment/{Assignid}/{studentid}")]
        public async Task<IActionResult> GetStudentAssignment(int Assignid, string studentid)
        {
            var assign = await _context.Assignments.FindAsync(Assignid);
            var student = await _context.Users.FindAsync(studentid);

            if (assign is null || student is null)
                return NotFound("Data is not found");

            var studentAssignment = await _context.SubmissionAssignments.Where(e => e.ApplicationUserId == student.Id && e.AssignmentId == assign.Id).FirstOrDefaultAsync();
            var data = new {
                student.FirstName,
                student.LastName,
                student.Id,student.Email,
                studentAssignment
            
            };


            return Ok(data);

        }



    }
}
