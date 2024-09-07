using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreRentalWriteRepository : EfCoreBaseWriteRepository<Rental>, IEfCoreRentalWriteRepository
    {
        public EfCoreRentalWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }


        public override Task UpdateAsync(Rental entity)
        {

            _table.Attach(entity);

            var entityEntry = _table.Entry(entity);

            entityEntry.Property(p => p.CarId).IsModified = true;
            entityEntry.Property(p => p.UserId).IsModified = true;
            entityEntry.Property(p => p.HourlyRentalPrice).IsModified = true;
            entityEntry.Property(p => p.TotalRentalPrice).IsModified = true;
            entityEntry.Property(p => p.UpdatedDate).IsModified = true;
            entityEntry.Property(p => p.RentalEndDate).IsModified = true;
            entityEntry.Property(p => p.RentalStartedDate).IsModified = true;
            entityEntry.Property(p => p.Status).IsModified = true;


            return Task.CompletedTask;

        }

    }
}
