using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Interfaces;

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

        public async Task<bool> Edit(Claim claim)
        {
            var existingClaim = await _claimRepository.GetByIdAsync(claim.Id);
            if (existingClaim == null)
            {
                return false;
            }
            existingClaim.Status = claim.Status;
            existingClaim.OrderId = claim.OrderId;
            await _claimRepository.UpdateAsync(existingClaim);
            return true;
        }

        public Task<List<Claim>> GetAll()
        {
            return _claimRepository.GetAllAsync(includes: x => x.Order);
        }

        public async Task<List<Claim>> GetByCustomerId(int customerId)
        {
            var claims = await _claimRepository.GetAllAsync(
                includes: x => x.Order);

            return claims
                .Where(c => c.Order != null && c.Order.CustomerId == customerId)
                .ToList();
        }

        public Task<Claim> GetById(int id)
        {
            return _claimRepository.GetByIdAsync(id, includes: x => x.Order);
        }

        public async Task<bool> SetStatus(int id, string status)
        {
            var claim = await _claimRepository.GetByIdAsync(id);

            if (claim == null)
            {
                return false;
            }

            claim.Status = status;
            claim.CompletedOn = status == "Completed" ? DateTime.UtcNow : null;

            await _claimRepository.UpdateAsync(claim);
            return true;
        }
    }
}
