using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequestHandler : IRequestHandler<DeleteBrandCommandRequest, DeleteBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IValidator<DeleteBrandCommandRequest> _validator;
        private readonly ILogger<DeleteBrandCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository,
            IBrandOutboxRepository brandOutboxRepository,
            IValidator<DeleteBrandCommandRequest> validator,
            ILogger<DeleteBrandCommandRequestHandler> logger,
            IMapper mapper)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _brandOutboxRepository = brandOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DeleteBrandCommandResponse> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new DeleteBrandCommandResponse { };
            }

            var isExists = await _brandReadRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new DeleteBrandCommandResponse { };

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();

            mongoSession.StartTransaction();

            var brandDeletedEvent = new BrandDeletedEvent
            {
                DeletedDate = DateTime.UtcNow,
                Id = request.Id,
            };

            try
            {
                _brandWriteRepository.DeleteById(request.Id);
                _brandWriteRepository.SaveChanges();

                await _brandOutboxRepository.AddMessageAsync(
                      new BrandOutboxMessage
                      {
                          Id = Guid.NewGuid(),
                          AddedDate = DateTime.Now,
                          IsPublished = false,
                          PublishDate = null,
                          Payload = brandDeletedEvent.Serialize()!
                      }, mongoSession);

                efTransaction.Commit();
                await mongoSession.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();
                throw;
            }


            return new DeleteBrandCommandResponse { };
        }
    }

}
