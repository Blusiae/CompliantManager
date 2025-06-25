using CompliantManager.Server.Data.Entities;
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

        public async Task InitializeAsync(string email, string password, string firstName, string lastName)
        {
            if (IsInitialized())
                return;

            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                await _roleManager.CreateAsync(new ApplicationRole("Administrator"));
            }

            var user = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName };
            var result = await _userManager.CreateAsync(user, password);

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
