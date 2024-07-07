using login_and_register.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
using System.Threading.Tasks;

namespace login_and_register.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IConnectionService _userConnectionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationService(ApplicationDbContext context, IHubContext<NotificationHub> hubContext, IConnectionService userConnectionService,UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userConnectionService = userConnectionService;
            _userManager = userManager;
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

            var Receiver =await _userManager.FindByIdAsync(userId);
            var ReceiverUserName = Receiver?.UserName;
            await _context.UserNotificationss.AddAsync(userNotification);
            await _context.SaveChangesAsync();

            var connectionId = _userConnectionService.GetConnectionIdByUserName(ReceiverUserName);
            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", subject, content);
            }
        }
    }
}
