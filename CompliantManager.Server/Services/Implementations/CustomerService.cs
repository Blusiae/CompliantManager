using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Interfaces;

namespace CompliantManager.Server.Services.Implementations
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task Create(Customer customer)
        {
            await _customerRepository.CreateAsync(customer);
        }

        public async Task<bool> Delete(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return false;
            }
            await _customerRepository.DeleteAsync(id);
            return true;
        }

        public async Task Edit(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task<List<Customer>> GetAll(int skip, int take)
        {
            return await _customerRepository.GetAllAsync(skip, take);
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _customerRepository.GetByIdAsync(id, includes: x => x.Address);
        }

        public async Task<int> GetCount()
        {
            return await _customerRepository.GetCountAsync();
        }
        public async Task<bool> SetNotifications(int id, bool notificationsEnabled)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return false;
            }
            customer.NotificationsEnabled = notificationsEnabled;
            await _customerRepository.UpdateAsync(customer);
            return true;
        }
    }
}
