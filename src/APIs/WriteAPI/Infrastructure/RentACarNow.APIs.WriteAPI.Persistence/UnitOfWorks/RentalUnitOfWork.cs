using Microsoft.EntityFrameworkCore.Storage;
using RentACarNow.APIs.WriteAPI.Application.Interfaces.UnitOfWorks;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;

namespace RentACarNow.APIs.WriteAPI.Persistence.UnitOfWorks
{
    public class RentalUnitOfWork : IRentalUnitOfWork, IDisposable
    {

        private readonly IEfCoreCarReadRepository _efCoreCarReadRepository;
        private readonly IEfCoreCarWriteRepository _efCoreCarWriteRepository;
        private readonly IEfCoreUserReadRepository _efCoreUserReadRepository;
        private readonly IEfCoreUserWriteRepository _efCoreUserWriteRepository;
        private readonly IEfCoreRentalReadRepository _efCoreRentalReadRepository;
        private readonly IEfCoreRentalWriteRepository _efCoreRentalWriteRepository;
        private IDbContextTransaction _transaction;
        private readonly RentalACarNowDbContext _context;

        public RentalUnitOfWork(
            IEfCoreCarReadRepository efCoreCarReadRepository,
            IEfCoreCarWriteRepository efCoreCarWriteRepository,
            IEfCoreUserReadRepository efCoreUserReadRepository,
            IEfCoreUserWriteRepository efCoreUserWriteRepository,
            IEfCoreRentalReadRepository efCoreRentalReadRepository,
            IEfCoreRentalWriteRepository efCoreRentalWriteRepository,
            RentalACarNowDbContext context)
        {
            _efCoreCarReadRepository = efCoreCarReadRepository;
            _efCoreCarWriteRepository = efCoreCarWriteRepository;
            _efCoreUserReadRepository = efCoreUserReadRepository;
            _efCoreUserWriteRepository = efCoreUserWriteRepository;
            _efCoreRentalReadRepository = efCoreRentalReadRepository;
            _efCoreRentalWriteRepository = efCoreRentalWriteRepository;
            _context = context;
        }

        public IEfCoreCarReadRepository CarReadRepository => _efCoreCarReadRepository;
        public IEfCoreCarWriteRepository CarWriteRepository => _efCoreCarWriteRepository;
        public IEfCoreUserReadRepository UserReadRepository => _efCoreUserReadRepository;
        public IEfCoreUserWriteRepository UserWriteRepository => _efCoreUserWriteRepository;
        public IEfCoreRentalReadRepository RentalReadRepository => _efCoreRentalReadRepository;
        public IEfCoreRentalWriteRepository RentalWriteRepository => _efCoreRentalWriteRepository;

        public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                Dispose();
            }


        }
        public async Task RollbackAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                Dispose();
            }

        }
        public void Rollback()
        {
            if (_transaction is not null)
            {
                _transaction.Rollback();
                Dispose();
            }
        }
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }


        }
        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();
        public void Dispose() => _transaction?.Dispose();

    }
}
