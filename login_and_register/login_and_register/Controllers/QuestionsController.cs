using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateQuestion(int id,[FromBody] QuestionsModel question)
        {
            

            if (question == null || !await _context.Exams.AllAsync(e=>e.Id==id))
                return NotFound("Model is not found");

            var createdquestion = new Question
            {
                ExamId = id,
                Text = question.Text,
                Options = question.Options,
                CorrectAnswer = question.CorrectAnswer,
                Explanation = question.Explanation
            };

            await _context.Questions.AddAsync(createdquestion);
            _context.SaveChanges();

            return Ok(createdquestion);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            if (!await _context.Questions.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var question = await _context.Questions.FindAsync(id);

            if (question == null)
                return NotFound("question is not found");

            return Ok(question);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionsModel quest)
        {
        

            var question = await _context.Questions.FindAsync(id);

            if (quest == null || question == null)
                return NotFound("Model is not found");

            question.Text = quest.Text;
            question.Options = quest.Options;
            question.CorrectAnswer = quest.CorrectAnswer;
            question.Explanation = quest.Explanation;

            _context.SaveChanges();

            return Ok(question);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (!await _context.Questions.AnyAsync(q => q.Id == id))
                return NotFound("id is not found");

            var question = await _context.Questions.FindAsync(id);

            if (question == null)
                return NotFound("Model is not found");

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return Ok(question);

        }
    }
}
