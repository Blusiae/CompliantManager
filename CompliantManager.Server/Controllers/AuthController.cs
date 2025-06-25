using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompliantManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthService authService) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IAuthService _authService = authService;

        [Authorize(Roles = "Administrator")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email, FirstName = dto.FirstName!, LastName = dto.LastName! };
            var result = await _userManager.CreateAsync(user, dto.Password!);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(user.Id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email!);
            if (user == null)
                return Unauthorized("Nieprawidłowy login lub hasło.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password!, false);
            if (!result.Succeeded)
                return Unauthorized("Nieprawidłowy login lub hasło.");

            var token = await _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user is null)
                return NotFound("Użytkownik nie znaleziony.");

            return Ok(new { user.Id, user.Email, user.UserName });
        }
    }
}
