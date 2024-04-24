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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrades(int id)
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

        [HttpGet("{examid}/{studentid}")]
        public async Task<IActionResult> GetStudentExam(int examid,string studentid )
        {
            var exam = await _context.Exams.FindAsync(examid);
            var student = await _context.Users.FindAsync(studentid);

            if (exam is null || student is null)
                return NotFound("Data is not found");

            var studentExam = await _context.Submissions.Include(e=>e.Exam.Questions).Where(e => e.ApplicationUserId == student.Id).Select(e=>e.Exam).ToListAsync();
            var data = new {student,studentExam };
            

            return Ok(data);

        }
    }
}
