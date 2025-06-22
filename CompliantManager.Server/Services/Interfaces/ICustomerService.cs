using CompliantManager.Server.Data.Entities;

namespace CompliantManager.Server.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetById(int id);
        Task<List<Customer>> GetAll();
        Task<Customer> Create(Customer customer);
        Task<bool> Edit(Customer customer);
        Task<bool> Delete(int id);
        Task<bool> SetNotifications(int id, bool notificationsEnabled);
    }
}
