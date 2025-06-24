using CompliantManager.Server.Data.Entities;

namespace CompliantManager.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(ApplicationUser user);
    }
}
