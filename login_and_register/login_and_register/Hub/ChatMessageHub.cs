using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;
using login_and_register.Models;
using Microsoft.AspNetCore.Identity;
using login_and_register.Dtos;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using login_and_register.Helpers;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private static Dictionary<string, UserConnectionInfo> connectedUsers = new Dictionary<string, UserConnectionInfo>();
    public override Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        string userName = httpContext.Request.Query["customData"].ToString();

        connectedUsers[Context.ConnectionId] = new UserConnectionInfo { ConnectionId = Context.ConnectionId, UserName = userName};

        return base.OnConnectedAsync();
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

    public string? GetUserNameByConnectionId(string connectionId)
    {
        if (connectedUsers.TryGetValue(connectionId, out var userConnectionInfo))
        {
            return userConnectionInfo.UserName;
        }
        return null;
    }

    //public List<string> GetConnectionIdsByGroupName(string groupName)
    //{
    //    var connectionIds = new List<string>();

    //    foreach (var user in connectedUsers)
    //    {
    //        if (user.Value.GroupName == groupName)
    //        {
    //            connectionIds.Add(user.Key);
    //        }
    //    }

    //    return connectionIds;
    //}


    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (connectedUsers.ContainsKey(Context.ConnectionId))
        {
            connectedUsers.Remove(Context.ConnectionId);
        }

        return base.OnDisconnectedAsync(exception);
    }


    public async Task SendMessage(string ReceiverUserName, string? message, int? fileid = null)
    {


        var senderConnectionId = Context.ConnectionId;

        var senderUserName = GetUserNameByConnectionId(senderConnectionId);

        var user = GetConnectionIdByUserName(ReceiverUserName);

        var sender = await _userManager.FindByNameAsync(senderUserName);
        var receiver = await _userManager.FindByNameAsync(ReceiverUserName);
        
        
        if (string.IsNullOrEmpty(user))
            await Clients.Caller.SendAsync("ReceiveMessage", sender.UserName, message, fileid);
        else
        {
            await Clients.Client(user).SendAsync("ReceiveMessage", sender.UserName, message, fileid);
            await Clients.Caller.SendAsync("ReceiveMessage", sender.UserName, message, fileid);
        }


        

        if (fileid == null && string.IsNullOrEmpty(message))
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Cannot send an empty message and no file.");
            return;
        }

        if (fileid == null)
        {

            var mess = new ChatMessage
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Message = message,
                File = null,
                Timestamp = DateTime.UtcNow
            };

            await _context.ChatMessages.AddAsync(mess);

        }

        else if (fileid != null)
        {
            var mess = await _context.ChatMessages.FindAsync(fileid);
            if (mess == null)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "Message not found.");
                return;
            }

            mess.Message = message ?? mess.Message;
            mess.Timestamp = DateTime.UtcNow;
            _context.ChatMessages.Update(mess);
        }

        await _context.SaveChangesAsync();

    }




    public async Task CreateGroup(string username, string groupName)
    {
        var owner = await _userManager.FindByNameAsync(username);
        if (owner == null)
        {
            await Clients.Caller.SendAsync("CreateGroupMessage", "System", "Owner not found.");
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

        await _context.ChatGroups.AddAsync(group);
        await _context.SaveChangesAsync();

        await Clients.Caller.SendAsync("GroupCreated", group.Id, groupName);
    }



    public async Task AddMemberToGroup(int groupId, List<string> userNames, bool isAdmin)
    {
        var group = await _context.ChatGroups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
        if (group == null)
        {
            await Clients.Caller.SendAsync("AddmemberMessage", "System", "Group not found.");
            return;
        }


        if (userNames == null)
        {
            await Clients.Caller.SendAsync("AddmemberMessage", "System", " Users not found.");
            return;
        }

        foreach (var member in userNames)
        {

            var user = await _userManager.FindByNameAsync(member);

            if (user == null)
            {
                await Clients.Caller.SendAsync("AddmemberMessage", "System", "User not found.");
                return;
            }

            if (group.Members.Any(m => m.UserId == user.Id))
            {
                await Clients.Caller.SendAsync("AddmemberMessage", "System", "User already in the group.");
                return;
            }

            group.Members.Add(new ChatGroupMember { UserId = user.Id, IsAdmin = isAdmin });
            await _context.SaveChangesAsync();
            var connectionId = GetConnectionIdByUserName(user.UserName);
            await Clients.Caller.SendAsync("MemberAdded", groupId, user.UserName, group.Members.ToList());
            if (connectionId != null)
            {
                await Clients.Client(connectionId).SendAsync("AddedToGroup", groupId, group.Name, group.Members.ToList());
            }
        }
  
        


    }




    public async Task RemoveMemberFromGroup(int groupId, string userName)
    {
        var group = await _context.ChatGroups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
        if (group == null)
        {
            await Clients.Caller.SendAsync("RemoveMemberMessage", "System", "Group not found.");
            return;
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            await Clients.Caller.SendAsync("RemoveMemberMessage", "System", "User not found.");
            return;
        }

        var member = group.Members.FirstOrDefault(m => m.UserId == user.Id);
        if (member == null)
        {
            await Clients.Caller.SendAsync("RemoveMemberMessage", "System", "User not in the group.");
            return;
        }

        group.Members.Remove(member);
        await _context.SaveChangesAsync();

        var connectionId = GetConnectionIdByUserName(user.UserName);
        await Clients.Caller.SendAsync("MemberRemoved", groupId, userName, group.Members.ToList());
        if (connectionId != null)
        {
            await Clients.Client(connectionId).SendAsync("RemovedFromGroup", groupId, group.Name, group.Members.ToList());
        }
    }



    public async Task SendGroupMessage(int groupId, string message, int? fileid = null)
    {
        try
        {
            var group = await _context.ChatGroups
                .Include(g => g.Members).ThenInclude(m => m.User).FirstOrDefaultAsync(g => g.Id == groupId);

            var senderConnectionId = Context.ConnectionId;
            var senderUserName = GetUserNameByConnectionId(senderConnectionId);
            var sender = await _userManager.FindByNameAsync(senderUserName);

            if (group != null)
            {
                foreach (var member in group.Members)
                {
                    var connectionId = GetConnectionIdByUserName(member.User.UserName);
                    if (!string.IsNullOrEmpty(connectionId))
                    {
                        await Clients.Client(connectionId).SendAsync("ReceiveGroupMessage", senderUserName, message,fileid);
                    }
                }

                if (fileid == null)
                {

                    var mess = new ChatMessage
                    {
                        SenderId = sender.Id,
                        Message = message,
                        GroupId = groupId,
                        File = null,
                        Timestamp = DateTime.UtcNow
                    };

                    await _context.ChatMessages.AddAsync(mess);

                }

                else if (fileid != null)
                {
                    var mess = await _context.ChatMessages.FindAsync(fileid);
                    if (mess == null)
                    {
                        await Clients.Caller.SendAsync("ReceiveMessage", "System", "Message not found.");
                        return;
                    }

                    mess.Message = message ?? mess.Message;
                    mess.Timestamp = DateTime.UtcNow;
                    mess.GroupId = groupId;
                    _context.ChatMessages.Update(mess);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveGroupMessage", "System", "Group or sender not found.");
            }
        }
        catch (Exception ex)
        {
            
            await Clients.Caller.SendAsync("ReceiveGroupMessage", "System", $"Error in SendGroupMessage: {ex.Message}");
        }
    }

}


