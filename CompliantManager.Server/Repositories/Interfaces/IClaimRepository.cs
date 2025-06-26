using CompliantManager.Server.Data.Entities;
using System.Linq.Expressions;

namespace CompliantManager.Server.Repositories.Interfaces
{
    public interface IClaimRepository : IBaseRepository<Claim>
    {
        Task<List<Claim>> GetByCustomerIdAsync(int skip, int take, int customerId);
        Task<List<Claim>> GetAllAsync(int skip, int take, Expression<Func<Claim, bool>>? filter = null, params Expression<Func<Claim, object>>[] includes);
        Task<Claim?> GetByIdAsync(int id, Expression<Func<Claim, Claim>>? selector = null, params Expression<Func<Claim, object>>[] includes);
        Task<Claim?> GetByIdAsNoTracking(int id);
    }
}
