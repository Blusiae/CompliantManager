using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Interfaces;

namespace CompliantManager.Server.Services.Implementations
{
    public class ClaimService(IClaimRepository claimRepository, ICustomerRepository customerRepository) : IClaimService
    {
        private readonly IClaimRepository _claimRepository = claimRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
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
            return _claimRepository.GetAllAsync();
        }

        public async Task<List<Claim>> GetByCustomerId(int customerId)
        {
            return await _claimRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<Claim?> GetById(int id)
        {
            return await _claimRepository.GetByIdAsync(id);
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