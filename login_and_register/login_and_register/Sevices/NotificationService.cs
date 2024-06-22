using login_and_register.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace login_and_register.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(string userId, string subject, string content)
        {
            
            var notification = new Notification
            {
                Subject = subject
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            
            var userNotification = new UserNotification
            {
                ApplicationUserId = userId,
                NotificationId = notification.Id,
                Content = content
            };
            await _context.UserNotificationss.AddAsync(userNotification);
            await _context.SaveChangesAsync();

            
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", subject, content);
        }

       
    }
}
