﻿using RentACarNow.Application.Interfaces.Repositories.Base;
using RentACarNow.Domain.Entities.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Interfaces.Repositories.Read.Mongo
{
    public interface IMongoCarReadRepository : IMongoReadRepository<Car>
    {
    }
  

}