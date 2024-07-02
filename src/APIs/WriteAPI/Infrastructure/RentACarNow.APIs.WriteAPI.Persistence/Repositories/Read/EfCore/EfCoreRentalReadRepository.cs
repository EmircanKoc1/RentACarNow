﻿using RentACarNow.APIs.WriteAPI.Persistence.Contexts.EfCoreContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Application.Interfaces.Repositories.Read.EfCore;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Read.EfCore
{
    public class EfCoreRentalReadRepository : EfCoreBaseReadRepository<Rental>, IEfCoreRentalReadRepository
    {
        public EfCoreRentalReadRepository(RentalACarNowDbContext context) : base(context)
        {
        }
    }
}
