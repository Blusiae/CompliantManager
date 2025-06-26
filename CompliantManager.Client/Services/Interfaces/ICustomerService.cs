using CompliantManager.Shared.Dtos;

namespace CompliantManager.Client.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync(int count, int offset);
        Task<CustomerDto> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task DeleteManyAsync(List<int> ids);
        Task DeleteByIdAsync(int id);
        Task<int> CreateAsync(CustomerDto customer);
        Task UpdateAsync(CustomerDto customer);
    }
}
