using login_and_register.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(string userId)
        {
            var notifications = await _context.UserNotificationss
                                              .Where(u => u.ApplicationUserId == userId)
                                              .Include(u => u.Notification)
                                              .OrderByDescending(u => u.Id) 
                                              .ToListAsync();

            if (notifications == null || notifications.Count == 0)
                return NotFound("No notifications found");

            return Ok(notifications);
        }
    }
}
