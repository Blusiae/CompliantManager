using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Enums;

namespace CompliantManager.Server.Services.Implementations
{
    public class ClaimService(IClaimRepository claimRepository) : IClaimService
    {
        private readonly IClaimRepository _claimRepository = claimRepository;
        public async Task Create(Claim claim)
        {
            await _claimRepository.CreateAsync(claim);
        }

        public async Task<bool> Delete(int id)
        {
            var claim = await _claimRepository.GetByIdAsync(id);
            if (claim == null)
            {
                return false;
            }
            await _claimRepository.DeleteAsync(id);
            return true;
        }

        public async Task Edit(Claim claim)
        {
            await _claimRepository.UpdateAsync(claim);
        }

        public Task<List<Claim>> GetAll(int skip, int take)
        {
            return _claimRepository.GetAllAsync(skip, take);
        }

        public async Task<List<Claim>> GetByCustomerId(int customerId)
        {
            return await _claimRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<Claim?> GetById(int id)
        {
            return await _claimRepository.GetByIdAsync(id);
        }

        public async Task<int> GetCount()
        {
            return await _claimRepository.GetCountAsync();
        }

        public async Task<bool> SetStatus(int id, Status status)
        {
            var claim = await _claimRepository.GetByIdAsync(id);

            if (claim == null)
            {
                return false;
            }

            claim.Status = status;
            claim.CompletedOn = status is Status.Zaakceptowane or Status.Odrzucone ? DateTime.UtcNow : null;

            await _claimRepository.UpdateAsync(claim);
            return true;
        }

        public Task<bool> SetStatus(int id, string status)
        {
            throw new NotImplementedException();
        }
    }
}