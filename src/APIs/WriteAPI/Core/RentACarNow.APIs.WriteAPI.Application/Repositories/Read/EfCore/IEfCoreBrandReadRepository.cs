﻿using RentACarNow.APIs.WriteAPI.Application.Repositories.Base;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore
{
    public interface IEfCoreBrandReadRepository : IEfReadRepository<Brand>
    {

        //Task<Brand?> GetBrandByIdWithCars(Guid id, bool tracking = false,);

    }

}
