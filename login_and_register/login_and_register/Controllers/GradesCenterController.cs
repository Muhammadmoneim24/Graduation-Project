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
        public async Task<IActionResult> GetStudentExam(int examid,string studentid )
        {
            var exam = await _context.Exams.FindAsync( examid);
            var student = await _context.Users.FindAsync(studentid);


            var separator = new char[] { '/',',' };
            var Questions = await _context.Questions
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

            if (exam is null || student is null)
                return NotFound("Exam or student is not found");

            var studentAnswers = await _context.Submissions
                .Where(s => s.ExamId == examid && s.ApplicationUserId == studentid)
                .Select(e => new {
                    e.ApplicationUserId,
                    e.QuestionId,
                    StudentAnswer = e.StudentAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList(),
                }).ToListAsync();


            var examWithStudentAnswers = new
            {
                exam,
                Questions = Questions.Select(q => new
                {
                    q.Id,
                    q.Type,
                    q.Text,
                    q.Options,
                    q.CorrectAnswer,
                    q.Points,
                    q.Explanation,
                    studentAnswers.FirstOrDefault(sa => sa.QuestionId == q.Id)?.StudentAnswer
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

            var studentAssignment = await _context.SubmissionAssignments.Where(e => e.ApplicationUserId == student.Id).FirstOrDefaultAsync(e => e.AssignmentId == assign.Id);
            var data = new { student, studentAssignment };


            return Ok(data);

        }



    }
}
