using CompliantManager.Server.Data;
using CompliantManager.Server.Data.Entities;
using CompliantManager.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompliantManager.Server.Repositories.Implementations
{
    public class ClaimRepository(ApplicationDbContext context) : BaseRepository<Claim>(context), IClaimRepository
    {
        protected override DbSet<Claim> DbSet => _context.Claims;
    }
}
