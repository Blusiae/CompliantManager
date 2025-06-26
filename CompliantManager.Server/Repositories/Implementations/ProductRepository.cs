using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompliantManager.Server.Repositories.Implementations
{
    public class ProductRepository(ApplicationDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
    {
        protected override DbSet<Product> DbSet => _context.Products;
    }
}
