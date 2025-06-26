using CompliantManager.Client.Services.Interfaces;
using CompliantManager.Shared.Enums;
using CompliantManager.Shared.Dtos;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace CompliantManager.Client.Services.Implementations
{
    public class ClaimService(HttpClient httpClient) : IClaimService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<int> CreateAsync(ClaimDto claim)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/claim", claim);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/claim/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono zgłoszenia do usunięcia.");
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteManyAsync(List<int> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/claim/deleteMultiple", ids);
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono zgłoszeń do usunięcia.");
            response.EnsureSuccessStatusCode();
        }

        public async Task<ClaimDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/claim/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ClaimDto>() ?? new();
        }

        public async Task<List<ClaimDto>> GetAllAsync(int count, int offset, ListMode listMode, Guid? userId = null)
        {
            var response = await _httpClient.GetAsync($"/api/claim?count={count}&offset={offset}&mode={listMode}&userId={userId}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return [];

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ClaimDto>>() ?? [];

        }

        public async Task<int> GetCountAsync()
        {
            var response = await _httpClient.GetAsync("/api/claim/count");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int?>() ?? 0;
        }

        public async Task UpdateAsync(ClaimDto claim)
        {
            var response = await _httpClient.PatchAsJsonAsync($"/api/claim", claim);
            response.EnsureSuccessStatusCode();
        }
    }
}
