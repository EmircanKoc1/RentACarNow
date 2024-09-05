using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreFeatureWriteRepository : EfCoreBaseWriteRepository<Feature>, IEfCoreFeatureWriteRepository
    {
        public EfCoreFeatureWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        public override Task UpdateAsync(Feature entity)
        {
            _context.Attach<Feature>(entity);

            var entityEntry = _context.Entry<Feature>(entity);

            entityEntry.Property(p => p.UpdatedDate).IsModified = true;
            entityEntry.Property(p => p.CarId).IsModified = true;
            entityEntry.Property(p => p.Name).IsModified = true;
            entityEntry.Property(p => p.Value).IsModified = true;

            return Task.CompletedTask;
        }

    }
}
