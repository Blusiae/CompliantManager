using CompliantManager.Shared.Dtos;

namespace CompliantManager.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync(int count, int offset);
        Task<UserDto> GetUserAsync(Guid id);
        Task<int> GetUsersCountAsync();
        Task DeleteUsersAsync(List<Guid> ids);
        Task DeleteUserAsync(Guid id);
        Task<Guid> CreateUserAsync(UserDto user);
        Task UpdateUserAsync(UserDto user);
    }
}
