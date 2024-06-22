using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using login_and_register.Models;
using Microsoft.AspNetCore.Identity;
using System;
using login_and_register.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task  SendMessage(MeassageModel message)
    {
        var sender = await _userManager.GetUserAsync(Context.User);
        var receiver = await _userManager.FindByNameAsync(message.receiverUserName);

        if (receiver == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "User not found.");
            return;
        }

        using var datastream = new MemoryStream();
        if (message.file != null)
        {
               
            await message.file.CopyToAsync(datastream);
        }

        var chatMessage = new ChatMessage
        {
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Message = message.message,
            File = message.file is null? null: datastream.ToArray(),
            Timestamp = DateTime.UtcNow
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        await Clients.User(receiver.Id).SendAsync("ReceiveMessage", sender.UserName, message);
        await Clients.Caller.SendAsync("ReceiveMessage", sender.UserName, message);
    }
}
