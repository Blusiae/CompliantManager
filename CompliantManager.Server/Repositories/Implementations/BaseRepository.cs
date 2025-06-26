using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompliantManager.Server.Repositories.Implementations
{
    public abstract class BaseRepository<T>(ApplicationDbContext dbContext) : IBaseRepository<T> where T : Entity
    {
        protected ApplicationDbContext _context = dbContext;
        protected abstract DbSet<T> DbSet { get; }

        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, Expression<Func<T, T>>? selector = null, params Expression<Func<T, object>>[] includes)
        {
            var baseQuery = GetQueryWithIncludes(includes);

            if (selector is not null)
            {
                baseQuery = baseQuery.Select(selector);
            }

            return await baseQuery.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, T>>? selector = null, params Expression<Func<T, object>>[] includes)
        {
            var baseQuery = GetQueryWithIncludes(includes);

            if (selector is not null)
            {
                baseQuery = baseQuery.Select(selector);
            }

            return await baseQuery
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var claim = await DbSet.FindAsync(id);
            if (claim != null)
            {
                DbSet.Remove(claim);
                await _context.SaveChangesAsync();
            }
        }

        protected IQueryable<T> GetQueryWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var baseQuery = DbSet.AsQueryable();

            if (includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    baseQuery = baseQuery.Include(include);
                }
            }

            return baseQuery;
        }

        public async Task<int> GetCountAsync()
        {
            return await DbSet.CountAsync();
        }
    }
}
