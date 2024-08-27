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

        //public async override Task UpdateAsync(Car entity)
        //{
        //    _context.Cars.Attach(entity);





        //}


    }
}
