﻿using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore
{
    public interface IEfCoreCustomerWriteRepository : IEfWriteRepository<Customer>
    {
    }
}
