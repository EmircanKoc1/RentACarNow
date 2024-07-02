﻿using RentACarNow.APIs.WriteAPI.Persistence.Contexts.MongoContexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
using RentACarNow.Application.Interfaces.Repositories.Write.Mongo;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.Mongo
{
    public class MongoCarWriteRepository : MongoBaseWriteRepository<Car>, IMongoCarWriteRepository
    {
        public MongoCarWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }
    }
}
