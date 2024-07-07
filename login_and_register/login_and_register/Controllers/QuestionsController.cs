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
        public async Task<IActionResult> CreateQuestion(int id,[FromBody] List<QuestionsModel> questions)
        {


            if (questions == null || !ModelState.IsValid)
                return NotFound("Model is not found");
 
            foreach (var question in questions) 
            {
                 var choices = "";
                foreach(var op in question.choices) 
                {
                    choices +="/"+op;
                }

                var answer = "";
                if(question.type == "multiple answers") 
                {
                    foreach(var ans in question.selectedAnswers) 
                    {
                        answer += "/" + ans;
                    } 

                }
                else 
                {
                    answer = question.correctAnswer;
                }

                
                var createdquestion = new Question
                {
                    ExamId = id,
                    Type = question.type,
                    Text = question.question,
                    Options = choices,
                    CorrectAnswer = answer,
                    Points = question.points,
                    Explanation = question.explanaition
                };



                await _context.Questions.AddAsync(createdquestion);

            }

            
           
            _context.SaveChanges();

           

            return Ok(questions);
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

        [HttpGet("QuestionsBank{courseId}")]
        public async Task<IActionResult> QuestionBank(int courseId)
        {
            var exam = await _context.Exams.Where(e => e.CourseId == courseId).Select(e=>e.Questions).ToListAsync();

            return Ok(exam);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionsModel quest)
        {
        

            var question = await _context.Questions.FindAsync(id);

            if (quest == null || question == null)
                return NotFound("Model is not found");

            question.Text = quest.question;
            question.Options = quest.choices.ToString();
            question.CorrectAnswer = quest.correctAnswer;
            question.Explanation = quest.explanaition;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

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
