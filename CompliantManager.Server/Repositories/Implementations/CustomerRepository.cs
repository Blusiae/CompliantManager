using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompliantManager.Server.Repositories.Implementations
{
    public class CustomerRepository(ApplicationDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
        protected override DbSet<Customer> DbSet => _context.Customers;
    }
}
