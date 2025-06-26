using CompliantManager.Server.Data.Entities;
using CompliantManager.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace CompliantManager.Server.Services.Implementations
{
    public class InitialSetupService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public bool IsInitialized()
        {
            return _userManager.Users.Any();
        }

        public async Task InitializeAsync(UserDto userDto)
        {
            if (IsInitialized())
                return;

            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                await _roleManager.CreateAsync(new ApplicationRole("Administrator"));
            }

            var user = new ApplicationUser { UserName = userDto.Email, Email = userDto.Email, FirstName = userDto.FirstName!, LastName = userDto.LastName! };
            var result = await _userManager.CreateAsync(user, userDto.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
            else
            {
                throw new Exception("Nie udało się utworzyć użytkownika: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
