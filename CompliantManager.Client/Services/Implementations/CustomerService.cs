using CompliantManager.Client.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace CompliantManager.Client.Services.Implementations
{
    public class CustomerService(HttpClient httpClient) : ICustomerService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<int> CreateAsync(CustomerDto customer)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/customer", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/customer/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono klienta do usunięcia.");
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteManyAsync(List<int> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/customer/deleteMultiple", ids);
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Nie znaleziono klientów do usunięcia.");
            response.EnsureSuccessStatusCode();
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/customer/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CustomerDto>() ?? new();
        }

        public async Task<List<CustomerDto>> GetAllAsync(int count, int offset)
        {
            var response = await _httpClient.GetAsync($"/api/customer?count={count}&offset={offset}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return [];

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<CustomerDto>>() ?? [];

        }

        public async Task<int> GetCountAsync()
        {
            var response = await _httpClient.GetAsync("/api/customer/count");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int?>() ?? 0;
        }

        public async Task UpdateAsync(CustomerDto customer)
        {
            var response = await _httpClient.PatchAsJsonAsync($"/api/customer", customer);
            response.EnsureSuccessStatusCode();
        }
    }
}
