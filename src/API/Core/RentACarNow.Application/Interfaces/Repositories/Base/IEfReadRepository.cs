﻿using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Interfaces.Repositories.Base
{
    public interface IEfReadRepository<TEntity> : IBaseReadRepository<TEntity> 
        where TEntity : BaseEntity, IEfEntity
    {
    }
}