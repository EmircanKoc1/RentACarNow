using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Repositories.Base
{
    public interface IEfWriteRepository<TEntity> : IBaseWriteRepository<TEntity>
        where TEntity : BaseEntity, IEFEntity
    {
        Task SaveChangesAsync();

    }
}
