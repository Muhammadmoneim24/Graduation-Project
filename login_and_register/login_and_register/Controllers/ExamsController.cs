using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
              EndDate = model.EndDate,
            };

            await _context.Exams.AddAsync(exam);
            _context.SaveChanges();

            return Ok(exam);
            
        }

        [HttpGet]
        public async Task<IActionResult> GetExam(int id)
        {

            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
                return NotFound("Exam is not found");

            return Ok(exam);

        }

        [HttpPut]
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

            _context.SaveChanges();

            return Ok(exam);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExam( int id)
        {
            if (!await _context.Exams.AnyAsync(e => e.Id == id))
                return NotFound("The Id is not found");

            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
                return NotFound("Exam is not found");

             _context.Exams.Remove(exam);
             _context.SaveChanges();

            return Ok(exam);

        }


       

    }
}
