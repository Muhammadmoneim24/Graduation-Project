using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace login_and_register.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateExam(int id,[FromBody] ExamModel model) 
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad Request");
            if(model == null)
                return NotFound("Model is not found");

            var exam = new Exam
            {
              CourseId= id,
              Tittle = model.Tittle,
              Describtion = model.Describtion,
              Grades = model.Grades,
              Time = model.Time,
              Instructions = model.Instructions,
              Date = model.EndDate,
              NumOfQuestions = model.NumOfQuestions,
            };

            var separator = new char[] { '/' };
            var questions =  exam.Questions.Where(e => e.ExamId == id)
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
                .ToList();
            var list = new {exam,questions };

            await _context.Exams.AddAsync(exam);
            _context.SaveChanges();

            return Ok(list);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
                return NotFound("Exam is not found");

            var separator = new char[] { '/' };

            var Questions = await _context.Questions
                .Where(e => e.ExamId == id)
                .Select(e => new {
                    e.Id,
                    e.ExamId,
                    e.Type,
                    e.Text,
                    Options = e.Options != null ? e.Options.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>(),
                    CorrectAnswer = e.CorrectAnswer != null ? e.CorrectAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToArray() : new string[] { },
                    e.Points,
                    e.Explanation
                })
                .ToListAsync();

            //var examWithCorrectAnswers = Questions.Select(q => new {
            //    q.Id,
            //    q.ExamId,
            //    q.Type,
            //    q.Text,
            //    q.Options,
            //    CorrectAnswer = q.CorrectAnswer
            //    .OrderByDescending(o => int.Parse(o))
            //    .ToArray(),
            //    q.Points,
            //    q.Explanation
            //}).ToList();



            return Ok(new { exam, Questions } );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamQuestions(int id)
        {
            if (!await _context.Exams.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var separator = new char[] { '/' };

            var questions = await _context.Questions
                .Where(e => e.ExamId == id)
                .Select(e => new {
                    e.Id,
                    e.ExamId,
                    e.Type,
                    e.Text,
                    Options = e.Options.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    CorrectAnswer = e.CorrectAnswer.Split(separator, StringSplitOptions.RemoveEmptyEntries)

                             .ToArray(),
                    e.Points,
                    e.Explanation
                })
                .ToListAsync();


            if (questions == null)
                return NotFound("question is not found");

            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseExams(int id)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var exams = await _context.Exams.Include(e=>e.Questions).Where(e => e.CourseId == id).ToListAsync();


            if (exams == null)
                return NotFound("The coure has no exams");

            return Ok(exams);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, [FromBody] ExamModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad Request");

            var exam = await _context.Exams.FindAsync(id);

            if (model == null || exam == null)
                return NotFound("Model is not found");

            exam.Tittle = model.Tittle;
            exam.Describtion = model.Describtion; 
            exam.Instructions = model.Instructions;
            exam.Grades = model.Grades;

            _context.SaveChanges();

            return Ok(exam);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam( int id)
        {
           
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
                return NotFound("Exam is not found");

             _context.Exams.Remove(exam);
             await _context.SaveChangesAsync();

            return Ok(exam);

        }


       

    }
}
