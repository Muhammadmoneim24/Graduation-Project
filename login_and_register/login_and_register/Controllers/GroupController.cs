using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using login_and_register.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace login_and_register.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        [HttpGet("GetGroupInfo{groupId}")]
        public async Task<IActionResult> GetGroupInfo(int groupId)
        {
            var group = await _context.ChatGroups
                .Include(g => g.Members)
                .ThenInclude(m => m.User)
                .Include(g => g.Owner)  
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
            {
                return NotFound(new { Message = "Group not found." });
            }

            var groupInfo = new
            {
                group.Id,
                group.Name,
                Owner = new { group.Owner.UserName, group.Owner.Id },  
                Members = group.Members.Select(m => new { m.User.UserName, m.IsAdmin })
            };

            return Ok(groupInfo);
        }

        [HttpGet("GetGroupMessages{groupId}")]
        public async Task<IActionResult> GetGroupMessages(int groupId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    m.Id,
                    m.Message,
                    m.Timestamp,
                    Sender = m.Sender.UserName,
                    m.File
                })
                .ToListAsync();

            if (messages == null || !messages.Any())
            {
                return NotFound(new { Message = "No messages found for this group." });
            }

            return Ok(messages);
        }
    }
}
