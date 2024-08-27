﻿using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore
{
    public class EfCoreCarReadRepository : EfCoreBaseReadRepository<Car>, IEfCoreCarReadRepository
    {
        public EfCoreCarReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
