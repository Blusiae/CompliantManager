using CompliantManager.Server.Data.Entities;
using System.Linq.Expressions;

namespace CompliantManager.Server.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, T>>? selector = null, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id, Expression<Func<T, T>>? selector = null, params Expression<Func<T, object>>[] includes);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> GetCountAsync();
    }
}
