using CompliantManager.Shared.Dtos;
using CompliantManager.Shared.Enums;

namespace CompliantManager.Client.Services.Interfaces
{
    public interface IClaimService
    {
        Task<List<ClaimDto>> GetAllAsync(int count, int offset, ListMode listMode, Guid? userId = null);
        Task<ClaimDto> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task DeleteManyAsync(List<int> ids);
        Task DeleteByIdAsync(int id);
        Task<int> CreateAsync(ClaimDto claim);
        Task UpdateAsync(ClaimDto claim);
    }
}
