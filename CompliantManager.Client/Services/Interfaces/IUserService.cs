using CompliantManager.Shared.Dtos;

namespace CompliantManager.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync(int count, int offset);
        Task<int> GetUsersCountAsync();
    }
}
