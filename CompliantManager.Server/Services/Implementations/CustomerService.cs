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

        public async Task<bool> Edit(Customer customer)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customer.Id);
            if (existingCustomer == null)
            {
                return false;
            }
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.AddressId = customer.AddressId;
            existingCustomer.NotificationsEnabled = customer.NotificationsEnabled;
            await _customerRepository.UpdateAsync(existingCustomer);
            return true;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _customerRepository.GetByIdAsync(id, includes: x => x.Address);
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
