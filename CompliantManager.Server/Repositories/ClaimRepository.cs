using CompliantManager.Server.Data;
using CompliantManager.Shared.Entities;
using CompliantManager.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompliantManager.Server.Repositories
{
    public class ClaimRepository(ApplicationDbContext context) : IBaseRepository<Claim>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Claim>> GetAllAsync()
        {
            return await _context.Claims
                .Include(c => c.Order)
                .ToListAsync();
        }

        public async Task<Claim?> GetByIdAsync(int id)
        {
            return await _context.Claims
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.ClaimId == id);
        }

        public async Task AddAsync(Claim entity)
        {
            await _context.Claims.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Claim entity)
        {
            _context.Claims.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
            }
        }
    }
}
