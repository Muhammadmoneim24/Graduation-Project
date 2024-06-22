

namespace login_and_register.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId, string subject, string content);
    }
}
