using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace CompliantManager.Server.Services.Implementations
{
    public class AuthService(IConfiguration configuration, UserManager<ApplicationUser> userManager) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
            ];

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
