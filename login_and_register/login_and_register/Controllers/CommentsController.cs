using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
      
        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(CommentModel comment) 
        {
            var comm = new Comment 
            {
                ApplicationUserId = comment.ApplicationUserId,
                DiscussionId = comment.DiscussionId,
                Content = comment.Content,
            };

            await _context.Comments.AddAsync(comm);
            await _context.SaveChangesAsync();

            return Ok(comm);
        }

        [HttpGet("GetPostComments{postid}")]
        public async Task<IActionResult> GetPostComments(int postid)
        {
           var post = await _context.Discussions.FindAsync(postid);
            if (post == null)
                return NotFound("Post is not found");

            var comments = await _context.Comments.Where(e => e.DiscussionId == postid).ToListAsync();
            

            return Ok(comments);
        }

        [HttpPut("EditComment{id}")]
        public async Task<IActionResult> EditComment(int id, CommentModel comment)
        {
            var comm = await _context.Comments.FindAsync(id);
            if (comm == null)
                return NotFound("Not found");

            comm.Content = comment.Content;

            _context.Comments.Update(comm);
            await _context.SaveChangesAsync();

            return Ok(comm);
        }

        [HttpDelete("DeleteComment{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comm = await _context.Comments.FindAsync(id);
            if (comm == null)
                return NotFound("Not found");

            _context.Comments.Remove(comm);
            await _context.SaveChangesAsync();

            return Ok("Removed");
        }
    }
}
