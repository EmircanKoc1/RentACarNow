using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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


        public override Task UpdateAsync(User entity)
        {
            _table.Attach(entity);

            var entityEntry = _table.Entry(entity);

            entityEntry.Property(u => u.Age).IsModified = true;
            entityEntry.Property(u => u.Email).IsModified = true;
            entityEntry.Property(u => u.Password).IsModified = true;
            entityEntry.Property(u => u.PhoneNumber).IsModified = true;
            entityEntry.Property(u => u.Surname).IsModified = true;
            entityEntry.Property(u => u.Name).IsModified = true;
            entityEntry.Property(u => u.WalletBalance).IsModified = true;

            return Task.CompletedTask;

        }

    }
}
