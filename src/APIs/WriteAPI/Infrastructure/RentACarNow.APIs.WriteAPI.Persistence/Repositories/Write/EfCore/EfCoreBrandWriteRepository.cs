using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreBrandWriteRepository : EfCoreBaseWriteRepository<Brand>, IEfCoreBrandWriteRepository
    {
        public EfCoreBrandWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        public async override Task UpdateAsync(Brand entity)
        {

            //var foundedEntity = _context.Brands.FindAsync(entity.Id);

            _context.Attach<Brand>(entity);

            var entityEntry = _context.Entry<Brand>(entity);

            entityEntry.Property(p => p.UpdatedDate).IsModified = true;
            entityEntry.Property(p => p.Name).IsModified = true;
            entityEntry.Property(p => p.Description).IsModified = true;


            await Task.CompletedTask;
        }


    }
}
