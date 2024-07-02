﻿using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Read.Mongo
{
    public class MongoCarReadRepository : MongoBaseReadRepository<Car>, IMongoCarReadRepository
    {
        public MongoCarReadRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }

}
