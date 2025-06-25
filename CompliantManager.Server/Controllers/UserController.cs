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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound("User not found.");
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdministrator = roles.Contains("Administrator")
            };
            return Ok(userDto);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetUsersCount()
        {
            return Ok(await _userManager.Users.CountAsync());
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            if (userDto is null || userDto.Id == Guid.Empty)
                return BadRequest("Invalid user data.");
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            if (user is null)
                return NotFound("User not found.");
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName!;
            user.LastName = userDto.LastName!;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (userDto.IsAdministrator && !currentRoles.Contains("Administrator"))
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
            else if (!userDto.IsAdministrator && currentRoles.Contains("Administrator"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrator");
            }
            return Ok("User updated successfully.");
        }

        [HttpPost("deleteMultiple")]
        public async Task<IActionResult> DeleteUsers([FromBody] List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("No user IDs provided.");
            var users = await _userManager.Users
                .Where(u => ids.Contains(u.Id))
                .ToListAsync();
            if (users.Count == 0)
                return NotFound("No users found to delete.");
            foreach (var user in users)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }
            return Ok("Users deleted successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound("User not found.");
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok("User deleted successfully.");
        }
    }
}
