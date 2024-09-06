using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreClaimWriteRepository : EfCoreBaseWriteRepository<Claim>, IEfCoreClaimWriteRepository
    {
        public EfCoreClaimWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }



        public override Task UpdateAsync(Claim entity)
        {
            _table.Attach(entity);

            var entityEntry = _table.Entry(entity);

            entityEntry.Property(p => p.UpdatedDate).IsModified = true;
            entityEntry.Property(p=>p.Key).IsModified = true;
            entityEntry.Property(p=>p.Value).IsModified = true;

            return Task.CompletedTask;
        }


    }



}
