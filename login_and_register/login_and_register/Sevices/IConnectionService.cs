using login_and_register.Hub;

public interface IConnectionService
{
    Task AddConnectedUserAsync(string connectionId, string userName);
    Task RemoveConnectedUserAsync(string connectionId);
    string? GetConnectionIdByUserName(string userName);
    
}

