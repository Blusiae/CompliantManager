using CompliantManager.Server.Data.Entities;

namespace CompliantManager.Server.Services.Interfaces
{
    public interface IClaimService
    {
        Task<Claim?> GetById(int id);
        Task<List<Claim>> GetAll(int skip, int take);
        Task Create(Claim claim);
        Task Edit(Claim claim);
        Task<bool> Delete(int id);
        Task<List<Claim>> GetByCustomerId(int customerId);
        Task<bool> SetStatus(int id, string status);
        Task<int> GetCount();
        // Task SetResponsibleConsultant(int claimId, int consultantId);
    }
}
