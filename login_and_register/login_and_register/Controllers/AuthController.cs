using login_and_register.Dtos;
using login_and_register.Models;
using login_and_register.Sevices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace login_and_register.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;

        public AuthController(IAuthService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);
            var userdata = await _context.Users.Include(e=>e.UserCourses).Where(e=>e.Email==model.Email).ToListAsync();

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var list = new {result,userdata };

            return Ok(list);
        }

        //[HttpPost("addrole")]
        //public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _authService.AddRoleAsync(model);

        //    if (!string.IsNullOrEmpty(result))
        //        return BadRequest(result);

        //    return Ok(model);
        //}

        [HttpPost("AutoLogin")]
        public async Task<IActionResult> AutoLogin()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length);
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtSecurityToken = handler.ReadJwtToken(token);

                    if (jwtSecurityToken != null)
                    {
                        var emailClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
                        if (emailClaim != null)
                        {
                            var userdata = await _context.Users.Include(e => e.UserCourses).Where(u => u.Email == emailClaim.Value).ToListAsync();
                            if (userdata != null)
                            {
                                return Ok(new { success = true,  userdata });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception
                    return BadRequest(new { success = false, error = ex.Message });
                }
            }

            return Unauthorized(new { success = false, error = "Invalid token" });
        }

    }
}
