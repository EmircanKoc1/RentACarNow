﻿using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoBrandWriteRepository : MongoBaseWriteRepository<Brand>, IMongoBrandWriteRepository
    {
        public MongoBrandWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
