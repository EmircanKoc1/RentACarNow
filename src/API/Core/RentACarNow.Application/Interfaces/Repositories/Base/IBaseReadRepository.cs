using RentACarNow.Application.Enums;
using RentACarNow.Application.Models;
using RentACarNow.Domain.Entities.Common.Concrete;

namespace RentACarNow.Application.Interfaces.Repositories.Base
{
    public interface IBaseReadRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = false);
        Task<TEntity?> GetFirstOrDefaultAsync(Guid id, bool tracking = false);
        Task<TEntity?> GetLastOrDefaultAsync(Guid id, bool tracking = false);
        Task<IEnumerable<TEntity?>?> GetAllAsync(PaginationParameters paginationParameters,
            bool tracking = false,
            OrderedDirection direction = OrderedDirection.None);



    }
}
