using CompliantManager.Server.Data.Entities;

namespace CompliantManager.Server.Repositories.Interfaces
{
    public interface IClaimRepository : IBaseRepository<Claim>
    {
        Task<List<Claim>> GetByCustomerIdAsync(int customerId);
    }
}
