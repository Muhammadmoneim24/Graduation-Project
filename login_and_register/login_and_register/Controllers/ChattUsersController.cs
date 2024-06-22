using login_and_register.Dtos;
using login_and_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("GetUserFriends")]
        public async Task<IActionResult> GetUserFriends(string UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null) return Conflict("User is not found");

            var friends = await _context.Users.Include(x => x.Friends).ToListAsync();

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
    }
}
