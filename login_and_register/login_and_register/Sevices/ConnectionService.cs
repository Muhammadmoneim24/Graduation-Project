using login_and_register.Hub;

public class ConnectionService : IConnectionService
{
    private static Dictionary<string, UserConnectionInfo> connectedUsers = new Dictionary<string, UserConnectionInfo>();

    public Task AddConnectedUserAsync(string connectionId, string userName)
    {
        connectedUsers[connectionId] = new UserConnectionInfo { ConnectionId = connectionId, UserName = userName };
        return Task.CompletedTask;
    }

    public Task RemoveConnectedUserAsync(string connectionId)
    {
        if (connectedUsers.ContainsKey(connectionId))
        {
            connectedUsers.Remove(connectionId);
        }
        return Task.CompletedTask;
    }

    public string? GetConnectionIdByUserName(string userName)
    {
        foreach (var user in connectedUsers)
        {
            if (user.Value.UserName == userName)
            {
                return user.Key;
            }
        }
        return null;
    }

   
}
