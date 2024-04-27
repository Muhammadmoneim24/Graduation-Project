using login_and_register.Dtos;
using login_and_register.Models;
using Humanizer;
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
        private  List<string> _allowedExtensions = new List<string> {".pdf",".doc", ".docx", ".png",".jpg" };
        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Createlecture(int id, [FromForm] LectureModel lecture)
        {
            if (lecture == null || !ModelState.IsValid)
                return NotFound("Model is not found");

            using var datastream = new MemoryStream();
            if (lecture.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(lecture.File.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");
                await lecture.File.CopyToAsync(datastream);
            }


                var lec = new Lecture
            {
                CourseId = id,
                Name = lecture.Name,
                Description = lecture.Description,
                Link = lecture.Link,
                LecFile = lecture.File is null? null: datastream.ToArray(),

            };

    

            await _context.Lectures.AddAsync(lec);
            _context.SaveChanges();

            return Ok(lec);
        }

        //[HttpPost("{id}")]
        //public async Task<IActionResult> Createlecture(int id,[FromBody]LectureModel lecture) 
        //{
        //    if (lecture == null || !ModelState.IsValid)
        //        return NotFound("Model is not found");

        //    var lec = new Lecture
        //    { 
        //        CourseId = id,
        //        Name = lecture.Name,
        //        Description = lecture.Description,
        //        Link = lecture.Link,

        //    };

        //    await _context.Lectures.AddAsync(lec);
        //    _context.SaveChanges();

        //    return Ok(lec);
        //}

        [HttpGet("GetCourseLectures{id}")]
        public async Task<IActionResult> GetCourseLectures(int id)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var lectures = await _context.Lectures.Where(e => e.CourseId == id).ToListAsync();


            if (lectures == null)
                return NotFound("Lectures are not found");

            return Ok(lectures);
        }

        [HttpGet("GetLecture{id}")]
        public async Task<IActionResult> Getlecture(int id)
        {
            if (!await _context.Lectures.AnyAsync(x => x.Id == id))
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

            if (lecture.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(lecture.File.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");

               
                using var dataStraem = new MemoryStream();

                await lecture.File.CopyToAsync(dataStraem);

                lec.LecFile = dataStraem.ToArray();
            }
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
            if (lec == null)
                return NotFound("Model is not found");

            _context.Lectures.Remove(lec);
            _context.SaveChanges();

            return Ok(lec);
        }
    }
}
