using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreUserWriteRepository : EfCoreBaseWriteRepository<User>, IEfCoreUserWriteRepository
    {
        public EfCoreUserWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        public async Task AddClaimToUser(Guid userId, Guid claimId)
        {
            var user = await _context.Users
                .Include(u => u.Claims)
                .FirstOrDefaultAsync(u => u.Id == userId);
            var claim = await _context.Claims.FindAsync(claimId);

            user.Claims.Add(claim);
        }

        public async Task DeleteClaimToUser(Guid userId, Guid claimId)
        {
            var user = await _context.Users
                .Include(u => u.Claims)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var claim = await _context.Claims.FindAsync(claimId);

            user.Claims.Remove(claim);
        }
    }
}
