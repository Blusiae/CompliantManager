using CompliantManager.Server.Data.Entities;
using CompliantManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompliantManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class UserController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int count, [FromQuery] int offset)
        {
            var users = await _userManager
                .Users
                .Skip(offset)
                .Take(count)
                .ToListAsync();

            if (users.Count == 0)
                return NotFound("No users found.");

            var userDtos = new List<UserDto>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                userDtos.Add(new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsAdministrator = roles.Contains("Administrator")
                });
            }

            return Ok(userDtos);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetUsersCount()
        {
            return Ok(await _userManager.Users.CountAsync());
        }
    }
}
