﻿using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoEmployeeWriteRepository : MongoBaseWriteRepository<Employee>, IMongoEmployeeWriteRepository
    {
        public MongoEmployeeWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public override Task UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }
    }

}
