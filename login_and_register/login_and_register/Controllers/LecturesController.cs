using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {   
        private readonly ApplicationDbContext _context;

        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Createlecture(int id,[FromBody]LectureModel lecture) 
        {
            if (lecture == null || !ModelState.IsValid)
                return NotFound("Model is not found");

            var lec = new Lecture
            { 
                Id = id,
                Name = lecture.Name,
                Description = lecture.Description,
                Link = lecture.Link,
            
            };

            await _context.Lectures.AddAsync(lec);
            _context.SaveChanges();

            return Ok(lec);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getlecture(int id) 
        {
            if(!await _context.Lectures.AnyAsync(x => x.Id == id))
                return BadRequest("Id is not valid");

            var lec = await _context.Lectures.FindAsync(id);

            if (lec == null)
                return NotFound("Lec is not found");

            return Ok(lec);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatelecture(int id, [FromBody] LectureModel lecture)
        {
             var lec = await _context.Lectures.FindAsync(id);

            if (lecture == null || lec == null)
                return NotFound("Model is not found");

            lec.Name = lecture.Name;
            lec.Description = lecture.Description;
            lec.Link = lecture.Link;     

            _context.SaveChanges();

            return Ok(lec);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletelecture(int id)
        {
            if (!await _context.Lectures.AnyAsync(q => q.Id == id))
                return NotFound("id is not found");

            var lec = await _context.Lectures.FindAsync(id);
            if ( lec == null)
                return NotFound("Model is not found");

            _context.Lectures.Remove(lec);
            _context.SaveChanges();

            return Ok(lec);
        }
    }
}
