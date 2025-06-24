using CompliantManager.Client.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using System.Net.Http.Json;

namespace CompliantManager.Client.Services.Implementations
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<UserDto>> GetUsersAsync(int count, int offset)
        {
            var response = await _httpClient.GetAsync($"/api/user?count={count}&offset={offset}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? [];
        }

        public async Task<int> GetUsersCountAsync()
        {
            var response = await _httpClient.GetAsync("/api/user/count");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int?>() ?? 0;
        }
    }
}
