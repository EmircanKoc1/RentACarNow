using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks
{
    public interface IRentalUnitOfWork
    {
        IEfCoreCarReadRepository CarReadRepository { get; }
        IEfCoreCarWriteRepository CarWriteRepository { get; }
        IEfCoreUserReadRepository UserReadRepository { get; }
        IEfCoreUserWriteRepository UserWriteRepository { get; }
        IEfCoreRentalReadRepository RentalReadRepository { get; }
        IEfCoreRentalWriteRepository RentalWriteRepository { get; }

        Task RollbackAsync();
        Task CommitAsync();
        Task BeginTransactionAsync();

        void Rollback();
        void Commit();
        void BeginTransaction();


    }
}
