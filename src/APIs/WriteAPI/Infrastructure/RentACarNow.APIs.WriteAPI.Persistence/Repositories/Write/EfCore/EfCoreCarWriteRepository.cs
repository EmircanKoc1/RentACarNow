using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreCarWriteRepository : EfCoreBaseWriteRepository<Car>, IEfCoreCarWriteRepository
    {
        public EfCoreCarWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        public override Task UpdateAsync(Car entity)
        {
            _table.Attach(entity);

            var entityEntry = _table.Entry(entity);

            entityEntry.Property(c => c.Name).IsModified = true;
            entityEntry.Property(c => c.Modal).IsModified = true;
            entityEntry.Property(c => c.Title).IsModified = true;
            entityEntry.Property(c => c.HourlyRentalPrice).IsModified = true;
            entityEntry.Property(c => c.Kilometer).IsModified = true;
            entityEntry.Property(c => c.Description).IsModified = true;
            entityEntry.Property(c => c.Color).IsModified = true;
            entityEntry.Property(c => c.PassengerCapacity).IsModified = true;
            entityEntry.Property(c => c.LuggageCapacity).IsModified = true;
            entityEntry.Property(c => c.FuelConsumption).IsModified = true;
            entityEntry.Property(c => c.CarFuelType).IsModified = true;
            entityEntry.Property(c => c.TransmissionType).IsModified = true;
            entityEntry.Property(c => c.ReleaseDate).IsModified = true;
            entityEntry.Property(c => c.IsRental).IsModified = true;
            entityEntry.Property(c => c.BrandId).IsModified = true;
            entityEntry.Property(c => c.UpdatedDate).IsModified = true;

            return Task.CompletedTask;

        }

    }
}
