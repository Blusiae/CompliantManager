using CompliantManager.Shared.Dtos;

namespace CompliantManager.Client.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetCustomersAsync(int count, int offset);
        Task<CustomerDto> GetCustomerAsync(int id);
        Task<int> GetCustomersCountAsync();
        Task DeleteCustomersAsync(List<int> ids);
        Task DeleteCustomerAsync(int id);
        Task<int> CreateCustomerAsync(CustomerDto customer);
        Task UpdateCustomerAsync(CustomerDto customer);
    }
}
