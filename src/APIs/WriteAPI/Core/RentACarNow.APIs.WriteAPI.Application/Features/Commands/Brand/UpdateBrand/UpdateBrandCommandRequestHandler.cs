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
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequestHandler : IRequestHandler<UpdateBrandCommandRequest, UpdateBrandCommandResponse>
    {
        private readonly IEfCoreBrandWriteRepository _brandWriteRepository;
        private readonly IEfCoreBrandReadRepository _brandReadRepository;
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IValidator<UpdateBrandCommandRequest> _validator;
        private readonly ILogger<UpdateBrandCommandRequestHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateBrandCommandRequestHandler(
            IEfCoreBrandWriteRepository brandWriteRepository,
            IEfCoreBrandReadRepository brandReadRepository, 
            IBrandOutboxRepository brandOutboxRepository, 
            IValidator<UpdateBrandCommandRequest> validator,
            ILogger<UpdateBrandCommandRequestHandler> logger, 
            IMapper mapper)
        {
            _brandWriteRepository = brandWriteRepository;
            _brandReadRepository = brandReadRepository;
            _brandOutboxRepository = brandOutboxRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UpdateBrandCommandResponse> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new UpdateBrandCommandResponse { };
            }

            var isExists = await _brandReadRepository.IsExistsAsync(request.Id);

            if (!isExists)
                return new UpdateBrandCommandResponse { };


            var brandEntity = _mapper.Map<EfEntity.Brand>(request);

            using var efTransaction = await _brandWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _brandOutboxRepository.StartSessionAsync();

            var brandUpdatedEvent = _mapper.Map<BrandUpdatedEvent>(brandEntity);
            try
            {
                mongoSession.StartTransaction();

                await _brandWriteRepository.UpdateAsync(brandEntity);
                await _brandWriteRepository.SaveChangesAsync();

                await _brandOutboxRepository.AddMessageAsync(new BrandOutboxMessage
                {
                    AddedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    IsPublished = false,
                    Payload = brandUpdatedEvent.Serialize()!,
                    PublishDate = DateTime.UtcNow,
                }, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();


            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();
                throw;
            }





            return new UpdateBrandCommandResponse { };
        }
    }

}
