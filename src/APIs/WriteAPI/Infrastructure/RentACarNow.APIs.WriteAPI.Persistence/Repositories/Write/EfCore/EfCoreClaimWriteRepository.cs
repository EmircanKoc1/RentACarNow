using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreClaimWriteRepository : EfCoreBaseWriteRepository<Claim>, IEfCoreClaimWriteRepository
    {
        public EfCoreClaimWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        public async Task AddClaimToAdminAsync(Guid claimId, Guid adminId)
        {
            var claim = await _context.Claims.FindAsync(claimId);

            //error
            //claim.Admins.Add(new Admin
            //{
            //    Id = adminId,
            //});

            var admin = await _context.Admins.FindAsync(adminId);

            admin.Claims.Add(claim);

            await _context.SaveChangesAsync();
        }

        public async Task AddClaimToCustomerAsync(Guid claimId, Guid customerId)
        {
            var claim = await _context.Claims.FindAsync(claimId);

            var customer = await _context.Admins.FindAsync(customerId);

            customer.Claims.Add(claim);

            await _context.SaveChangesAsync();
        }

        public async Task AddClaimToEmployeeAsync(Guid claimId, Guid employeeId)
        {
            var claim = await _context.Claims.FindAsync(claimId);

            var employee = await _context.Admins.FindAsync(employeeId);

            employee.Claims.Add(claim);

            await _context.SaveChangesAsync();
        }
    }



}
