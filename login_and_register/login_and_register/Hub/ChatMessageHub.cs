using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;
using login_and_register.Models;
using Microsoft.AspNetCore.Identity;
using login_and_register.Dtos;
using Microsoft.EntityFrameworkCore;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SendMessage(string senderusername,string receiverusername, string message)
    {

        //var sender = await _userManager.GetUserAsync(Context.User);
            var sender = await _userManager.FindByNameAsync(senderusername);
            var receiver = await _userManager.FindByNameAsync(receiverusername);

            if (receiver == null)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "User not found.");
                return;
            }



            await Clients.All.SendAsync("ReceiveMessage", senderusername, message);

            var chatMessage = new ChatMessage
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            using var memoryStream = new MemoryStream();
            //if (file != null)
            //{
            //    await file.CopyToAsync(memoryStream);
            //    chatMessage.File = memoryStream.ToArray();
                
            //}

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();


       
        //await Clients.User(sender.UserName).SendAsync("ReceiveMessage", sender.UserName, message);
        //wait Clients.User(receiver.UserName).SendAsync("ReceiveMessage", sender.UserName, message);
        //await Clients.Caller.SendAsync("ReceiveMessage", sender.UserName, message);

    }

    public async Task CreateGroup(string username,string groupName)
    {
        var owner = await _userManager.FindByNameAsync(username);
        if (owner == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Owner not found.");
            return;
        }

        var group = new ChatGroup
        {
            Name = groupName,
            OwnerId = owner.Id,
            Members = new List<ChatGroupMember>
            {
                new ChatGroupMember { UserId = owner.Id, IsAdmin = true }
            }
        };

        _context.ChatGroups.Add(group);
        await _context.SaveChangesAsync();

        await Clients.Caller.SendAsync("GroupCreated", group.Id, groupName);
    }



    public async Task AddMemberToGroup(int groupId, string userName, bool isAdmin)
    {
        var group = await _context.ChatGroups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
        if (group == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Group not found.");
            return;
        }

        var owner = await _userManager.GetUserAsync(Context.User);
        if (owner == null || owner.Id != group.OwnerId)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "You do not have permission to add members to this group.");
            return;
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "User not found.");
            return;
        }

        if (group.Members.Any(m => m.UserId == user.Id))
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "User already in the group.");
            return;
        }

        group.Members.Add(new ChatGroupMember { UserId = user.Id, IsAdmin = isAdmin });
        await _context.SaveChangesAsync();

        await Clients.Caller.SendAsync("MemberAdded", groupId, userName);
        await Clients.User(user.Id).SendAsync("AddedToGroup", groupId, group.Name);
    }




    public async Task RemoveMemberFromGroup(int groupId, string userName)
    {
        var group = await _context.ChatGroups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
        if (group == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Group not found.");
            return;
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "User not found.");
            return;
        }

        var member = group.Members.FirstOrDefault(m => m.UserId == user.Id);
        if (member == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "User not in the group.");
            return;
        }

        group.Members.Remove(member);
        await _context.SaveChangesAsync();

        await Clients.Caller.SendAsync("MemberRemoved", groupId, userName);
        await Clients.User(user.Id).SendAsync("RemovedFromGroup", groupId, group.Name);
    }



    public async Task SendGroupMessage(GroupMessage GMessage)
    {
        var sender = await _userManager.GetUserAsync(Context.User);
        if (sender == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Sender not found.");
            return;
        }

        var group = await _context.ChatGroups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == GMessage.groupId);
        if (group == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Group not found.");
            return;
        }

        using var datastream = new MemoryStream();
        if (GMessage.file != null)
        {

            await GMessage.file.CopyToAsync(datastream);
        }


        var chatMessage = new ChatMessage
        {
            SenderId = sender.Id,
            GroupId = group.Id,
            Message = GMessage.message,
            File = GMessage.file is null ? null : datastream.ToArray(),
            Timestamp = DateTime.UtcNow
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        var groupMembers = group.Members.Select(m => m.UserId).ToList();
        await Clients.Users(groupMembers).SendAsync("ReceiveGroupMessage", GMessage.groupId, sender.UserName, GMessage);

    }
}
