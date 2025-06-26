using CompliantManager.Server.Data.Entities;
using CompliantManager.Shared.Enums;

namespace CompliantManager.Server.Services.Interfaces
{
    public interface IClaimService
    {
        Task<Claim?> GetById(int id);
        Task<List<Claim>> GetAll(int skip, int take, ListMode mode, Guid? userId);
        Task Create(Claim claim);
        Task Edit(Claim claim);
        Task<bool> Delete(int id);
        Task<List<Claim>> GetByCustomerId(int skip, int take, int customerId);
        Task<int> GetCount();
        Task<Claim?> GetByIdAsNoTracking(int id);
    }
}
