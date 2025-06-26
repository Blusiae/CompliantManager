using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Enums;
using System.Linq.Expressions;

namespace CompliantManager.Server.Services.Implementations
{
    public class ClaimService(IClaimRepository claimRepository) : IClaimService
    {
        private readonly IClaimRepository _claimRepository = claimRepository;
        public async Task Create(Claim claim)
        {
            claim.CreatedOn = DateTime.Now;
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

        public Task<List<Claim>> GetAll(int skip, int take, ListMode listMode, Guid? userId)
        {
            Expression<Func<Claim, bool>>? condition = listMode switch
            {
                ListMode.Wszystkie => null,
                ListMode.Aktywne => c => c.Status == Status.Nowe || c.Status == Status.Procesowane,
                ListMode.Nieaktywne => c => c.Status == Status.Zaakceptowane || c.Status == Status.Odrzucone,
                ListMode.Moje => c => c.ConsultantId == userId,
                _ => null
            };
            return _claimRepository.GetAllAsync(skip, take, condition);
        }

        public async Task<List<Claim>> GetByCustomerId(int skip, int take, int customerId)
        {
            return await _claimRepository.GetByCustomerIdAsync(skip, take, customerId);
        }

        public async Task<Claim?> GetById(int id)
        {
            return await _claimRepository.GetByIdAsync(id);
        }
        public async Task<Claim?> GetByIdAsNoTracking(int id)
        {
            return await _claimRepository.GetByIdAsNoTracking(id);
        }

        public async Task<int> GetCount()
        {
            return await _claimRepository.GetCountAsync();
        }
    }
}