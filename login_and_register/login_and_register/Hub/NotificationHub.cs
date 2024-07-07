using login_and_register.Hub;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    private readonly IConnectionService _userConnectionService;

    public NotificationHub(IConnectionService userConnectionService)
    {
        _userConnectionService = userConnectionService;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        string userName = httpContext.Request.Query["customData"].ToString();

        await _userConnectionService.AddConnectedUserAsync(Context.ConnectionId, userName);

        await base.OnConnectedAsync();
    }

    public string? GetConnectionIdByUserName(string userName)
    {
        return _userConnectionService.GetConnectionIdByUserName(userName);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _userConnectionService.RemoveConnectedUserAsync(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
