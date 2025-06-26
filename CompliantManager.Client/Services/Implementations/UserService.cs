using CompliantManager.Client.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace CompliantManager.Client.Services.Implementations
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<UserDto>> GetUsersAsync(int count, int offset)
        {
            var response = await _httpClient.GetAsync($"/api/user?count={count}&offset={offset}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return [];

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? [];
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/user/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserDto>() ?? new();
        }

        public async Task<int> GetUsersCountAsync()
        {
            var response = await _httpClient.GetAsync("/api/user/count");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int?>() ?? 0;
        }

        public async Task DeleteUsersAsync(List<Guid> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/user/deleteMultiple", ids);
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono użytkowników do usunięcia.");
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/user/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono użytkownika do usunięcia.");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Guid> CreateUserAsync(UserDto user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid?>() ?? Guid.Empty;
        }

        public async Task UpdateUserAsync(UserDto user)
        {
            var response = await _httpClient.PatchAsJsonAsync($"/api/user", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var response = await _httpClient.GetAsync("/api/auth/me");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono bieżącego użytkownika.");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>() ?? new();
        }
    }
}
