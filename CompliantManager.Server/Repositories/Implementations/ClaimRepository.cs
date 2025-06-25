using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompliantManager.Server.Repositories.Implementations
{
    public class ClaimRepository(ApplicationDbContext context) : BaseRepository<Claim>(context), IClaimRepository
    {
        protected override DbSet<Claim> DbSet => _context.Claims;
        
        public override async Task<Claim?> GetByIdAsync(int id, Expression<Func<Claim, Claim>>? selector = null, params Expression<Func<Claim, object>>[] includes)
        {
            var baseQuery = GetQueryWithIncludes(includes);

            baseQuery.Include(x => x.Order)
                     .ThenInclude(o => o.Customer);

            if (selector is not null)
            {
                baseQuery = baseQuery.Select(selector);
            }

            return await baseQuery.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Claim>> GetByCustomerIdAsync(int customerId)
        {
            return await DbSet.Include(c => c.Order)
                .Where(claim => claim.Order.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
