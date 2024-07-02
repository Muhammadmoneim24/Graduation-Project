using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using login_and_register.Models;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Instructor")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetChats()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userChats = await _context.ChatMessages
                .Where(cm => cm.SenderId == userId || cm.ReceiverId == userId)
                .Select(cm => new
                {
                    OtherUser = cm.SenderId == userId ? cm.Receiver : cm.Sender,
                    LastMessageTimestamp = cm.Timestamp
                })
                .GroupBy(cm => cm.OtherUser.Id)
                .Select(group => new
                {
                    UserId = group.Key,
                    FullName = group.FirstOrDefault().OtherUser.FirstName + " " + group.FirstOrDefault().OtherUser.LastName,
                    LastMessageTimestamp = group.Max(cm => cm.LastMessageTimestamp)
                })
                .OrderByDescending(g => g.LastMessageTimestamp)
                .ToListAsync();

            return Ok(userChats);
        }

        [HttpGet("messages/{otheruserId}")]
      
        public async Task<IActionResult> GetMessages(string otheruserId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var messages = await _context.ChatMessages
                .Where(cm => (cm.SenderId == userId && cm.ReceiverId == otheruserId) ||
                             (cm.SenderId == otheruserId && cm.ReceiverId == userId))
                .OrderBy(cm => cm.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpGet("search")]
        
        public async Task<IActionResult> SearchUsersByName(string name)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var users = await _context.Users
                .Where(u => (u.FirstName + " " + u.LastName).Contains(name))
                .Select(u => new
                {
                    u.UserName,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
    
