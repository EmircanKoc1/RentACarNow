﻿using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Base;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo
{
    public interface IMongoBrandWriteRepository : IMongoWriteRepository<Brand>
    {

        //public Task UpdateCarInBrand(Guid brandId, Guid carId, Car entity);


    }

}
