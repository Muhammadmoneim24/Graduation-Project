using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private List<string> _allowedExtensions = new List<string> { ".pdf", ".doc", ".png", ".jpg" };

        public AsignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateAssignment{id}")]
        public async Task<IActionResult> CreateAssignment(int id, [FromForm]AssignmentModel assignment) 
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(assignment.File.FileName).ToLower()))
                return BadRequest("File extension is not allowed");

            if (assignment == null || !ModelState.IsValid)
                return NotFound("Model is not found");

            var datastream = new MemoryStream();
            await assignment.File.CopyToAsync(datastream);

            var assign = new Assignment
            {
              CourseId = id,
              Tittle = assignment.Tittle,
              Describtion = assignment.Description,
              Grade = assignment.Grade,
              File = datastream.ToArray(),
              EndDate = assignment.EndDate,
            };

            await _context.Assignments.AddAsync(assign);
            _context.SaveChanges();

            return Ok(assign);
        }

       

        [HttpGet("GetCourseAssignments{id}")]
        public async Task<IActionResult> GetCourseLectures(int id)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == id))
                return BadRequest("Id is not valid");

            var assignments = await _context.Assignments.Where(e => e.CourseId == id).ToListAsync();


            if (assignments == null)
                return NotFound("Lectures are not found");

            return Ok(assignments);
        }

        [HttpGet("GetAssignment{id}")]
        public async Task<IActionResult> GetAssignment(int id)
        {
            if (!await _context.Assignments.AnyAsync(x => x.Id == id))
                return BadRequest("Id is not valid");

            var assign = await _context.Assignments.FindAsync(id);

            if (assign == null)
                return NotFound("Lec is not found");

            return Ok(assign);
        }

        [HttpGet("GetAllAssignments")]
        public async Task<IActionResult> GetAllAssignments()
        {
            

            var assigns = await _context.Assignments.ToListAsync();

            if (assigns == null)
                return NotFound("No Assignments");

            return Ok(assigns);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromForm] AssignmentModel assignment)
        {
            var assign = await _context.Assignments.FindAsync(id);

            if (assignment == null || assign == null)
                return NotFound("Model is not found");

            if (assignment.File != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(assignment.File.FileName).ToLower()))
                    return BadRequest("File extension is not allowed");


                using var dataStraem = new MemoryStream();

                await assignment.File.CopyToAsync(dataStraem);

                assign.File = dataStraem.ToArray();
            }

            assign.Tittle = assignment.Tittle;
            assign.Grade = assignment.Grade;
            assign.Describtion = assignment.Description;
            assign.EndDate = assignment.EndDate;

            _context.Assignments.Update(assign);
            await _context.SaveChangesAsync();

            return Ok(assign);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            if (!await _context.Assignments.AnyAsync(q => q.Id == id))
                return NotFound("id is not found");

            var assign = await _context.Assignments.FindAsync(id);
            if (assign == null)
                return NotFound("Model is not found");

            _context.Assignments.Remove(assign);
            _context.SaveChanges();

            return Ok(assign);
        }
    }
}
