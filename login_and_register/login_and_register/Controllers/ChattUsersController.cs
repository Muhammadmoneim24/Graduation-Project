using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Drawing.Imaging;
using System.Security.Claims;

namespace login_and_register.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class ChattUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChattUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddFriend")]
        public  async Task<IActionResult> AddFriend([FromBody] AddFriendModel Friendmodel) 
        {
            var user = await _context.Users.FindAsync(Friendmodel.UserId);
            if (user == null) return Conflict("User is not found");

            var friend = await _context.Users.FindAsync(Friendmodel.FriendId);
            if (friend == null) return Conflict("Friend is not found");

            if (await _context.UserFriends.AnyAsync(uf => uf.UserId == Friendmodel.UserId && uf.FriendId == Friendmodel.FriendId))
                return Conflict("Friend is already added");

            var addfriend = new UserFriend { UserId = Friendmodel.UserId, FriendId = Friendmodel.FriendId };

            await _context.UserFriends.AddAsync(addfriend);
            await _context.SaveChangesAsync();

            return Ok(friend);


        }


        [HttpPost("SendMessageFile")]
        public async Task<IActionResult> SendMessageFile([FromForm] MessageFile messfile)
        {
            var user = await _context.Users.FindAsync(messfile.SenderId);
            if (user == null) return Conflict("User is not found");

            var friend = await _context.Users.FindAsync(messfile.ReceiverId);
            if (friend == null) return Conflict("Friend is not found");

            using var datastream = new MemoryStream();
            if (messfile.File != null)
            {
                await messfile.File.CopyToAsync(datastream);
            }

            var file = new ChatMessage
            {
                SenderId = messfile.SenderId,
                ReceiverId = messfile.ReceiverId,
                File = messfile.File is null ? null : datastream.ToArray()
            };

            await _context.ChatMessages.AddAsync(file);
            await _context.SaveChangesAsync();

            return Ok(file);


        }

        [HttpPost("SendGroupFile")]
        public async Task<IActionResult> SendGroupFile([FromForm] GroupFileModel gropfile)
        {
            var user = await _context.Users.FindAsync(gropfile.SenderId);
            if (user == null) return Conflict("User is not found");

            var group = await _context.ChatGroups.FindAsync(gropfile.GruopId);
            if (group == null) return Conflict("group is not found");

            using var datastream = new MemoryStream();
            if (gropfile.File != null)
            {
                await gropfile.File.CopyToAsync(datastream);
            }

            var file = new ChatMessage
            {
                SenderId = gropfile.SenderId,
                GroupId = gropfile.GruopId,
                File = gropfile.File is null ? null : datastream.ToArray()
            };

            await _context.ChatMessages.AddAsync(file);
            await _context.SaveChangesAsync();

            return Ok(file);


        }

        [HttpGet("GetUserGroups{userid}")]
        public async Task<IActionResult> GetUserGroups(string userid)
        {
            var groups = await _context.ChatGroupMembers
                .Where(m => m.UserId == userid)
                .Include(m => m.Group)
                .Include(m => m.Group)
                    .ThenInclude(g => g.Messages)
                        .ThenInclude(m => m.Sender)
                .Select(m => m.Group)
                .ToListAsync();

            if (groups == null || !groups.Any())
            {
                return NotFound("No groups");
            }

            var result = groups.Select(g => new
            {
                g.Id,
                g.Name,
                g.OwnerId,
                
                Messages = g.Messages.Select(m => new
                {
                    m.Id,
                    m.SenderId,
                    SenderUserName = m.Sender.UserName,
                    m.Message,
                    m.File,
                    m.Timestamp
                }).ToList()
            }).ToList();

            return Ok(result);
        }



        [HttpGet("GetGroupMembers{groupid}")]
        public async Task<IActionResult> GetGroupMembers(int groupid)
        {
            var groupmessages = await _context.ChatGroups.Where(e => e.Id == groupid).Select(e=>e.Messages).ToListAsync();
            var groupmembers = await _context.ChatGroupMembers.Include(e=>e.User).Where(e => e.GroupId == groupid).ToListAsync();

            if (groupmembers is null)
                return NotFound("No groups");


            return Ok(new { groupmembers , groupmessages });
        }

        [HttpGet("GetMessageFile{id}")]
        public async Task<IActionResult> GetMessageFile(int id )
        {
           
            var file = await _context.ChatMessages.FindAsync(id);
            return Ok(file.File);
        }


        [HttpGet("GetUserFriends{UserId}")]
        public async Task<IActionResult> GetUserFriends(string UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null) return Conflict("User is not found");

            var friends = await _context.UserFriends.Include(e=>e.Friend).Where(e=>e.UserId == UserId).ToListAsync();

            return Ok(friends);


        }


        [HttpGet("GetAllUserData")]
        public async Task<IActionResult> GetAllUserData() 
        {
            var users = await _context.Users.Include(e => e.UserCourses).ToListAsync();
            if (users is null)
                return NotFound("No Users");
            

            return Ok(users);
        }

        [HttpGet("GetUserData{username}")]
        public async Task<IActionResult> GetUserData(string username)
        {
            var user = await _context.Users.Include(e => e.UserCourses).FirstOrDefaultAsync(e=>e.UserName == username);
            if (user == null) return NotFound("User not found");


            return Ok(user);
        }

        [HttpGet("GetChatMessages")]
        public async Task<IActionResult> GetChatMessages(string senderid,string receiverid)
        {
            var user =  await _context.UserFriends.Where(e=>e.UserId == senderid && e.FriendId == receiverid).FirstOrDefaultAsync();
            if (user is null)
            {
                return Conflict("Friend is not found");
            }

            var messages = await _context.ChatMessages.Include(e=>e.Sender)
                .Where(cm => (cm.SenderId == senderid && cm.ReceiverId == receiverid) ||
                             (cm.SenderId == receiverid && cm.ReceiverId == senderid))
                .OrderBy(cm => cm.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpGet("GetGroupFile{groupId}")]
        public async Task<IActionResult> GetGroupFile(int groupId)
        {
            var group = await _context.ChatGroups.FindAsync(groupId);
            if (group is null)
            {
                return Conflict("group is not found");
            }

            var groupmessages = await _context.ChatMessages.Where(e => e.GroupId == groupId).Select(e=>e.File).FirstOrDefaultAsync();
            return Ok(groupmessages);
        }

        [HttpDelete("RemoveFriend")]
        public async Task<IActionResult> RemoveFriend( string friendid, string userid)
        {
            var friend = await _context.UserFriends.Where(e=>e.FriendId == friendid && e.UserId == userid).FirstOrDefaultAsync();
            if (friend == null) return Conflict("friend is not found");

            var deletedChats = await _context.ChatMessages
                .Where(e => (e.ReceiverId == friendid && e.SenderId == userid) || (e.ReceiverId == userid && e.SenderId == friendid))
                .ToListAsync();

            _context.ChatMessages.RemoveRange(deletedChats);

            _context.UserFriends.Remove(friend);
            await _context.SaveChangesAsync();

            return Ok( friend );


        }

    }
}
