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
        
        public async override Task<Claim?> GetByIdAsync(int id, Expression<Func<Claim, Claim>>? selector = null, params Expression<Func<Claim, object>>[] includes)
        {
            var baseQuery = GetQueryWithIncludes(includes);

            baseQuery = baseQuery
                .Include(x => x.Consultant)
                .Include(x => x.Order)
                    .ThenInclude(o => o.Customer)
                .Include(x => x.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product);

            if (selector is not null)
            {
                baseQuery = baseQuery.Select(selector);
            }

            var entity = await baseQuery.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<List<Claim>> GetAllAsync(int skip, int take, Expression<Func<Claim, bool>>? filter = null, params Expression<Func<Claim, object>>[] includes)
        {
            var query = GetQueryWithIncludes(includes);
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            return await query
                .Include(c => c.Consultant)
                .Include(c => c.Order)
                .ThenInclude(o => o.Customer)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Claim>> GetByCustomerIdAsync(int skip, int take, int customerId)
        {
            return await DbSet.Include(c => c.Order)
                .Where(claim => claim.Order.CustomerId == customerId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
        public async Task<Claim?> GetByIdAsNoTracking (int id)
        {
            return await DbSet.AsNoTracking()
                .Include(c => c.Order)
                    .ThenInclude(o => o.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
