using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("GetChatMessages{senderid,receiverid}")]
        public async Task<IActionResult> GetChatMessages(string senderid,string receiverid)
        {
            var user =  await _context.UserFriends.Where(e=>e.UserId == senderid && e.FriendId == receiverid).FirstOrDefaultAsync();
            if (user is null)
            {
                return Conflict("Friend is not found");
            }

            var messages = await _context.ChatMessages
                .Where(cm => (cm.SenderId == senderid && cm.ReceiverId == receiverid) ||
                             (cm.SenderId == receiverid && cm.ReceiverId == senderid))
                .OrderBy(cm => cm.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpDelete("RemoveFriend{friendid}")]
        public async Task<IActionResult> RemoveFriend(string friendid)
        {
            var friend = await _context.UserFriends.FindAsync(friendid);
            if (friend == null) return Conflict("friend is not found");

            _context.UserFriends.Remove(friend);
            await _context.SaveChangesAsync();

            return Ok(friend);


        }
    }
}
