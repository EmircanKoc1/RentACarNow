using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Feature;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar
{

    public class FeatureUpdateCarCommandHandler : IRequestHandler<FeatureUpdateCarCommandRequest, FeatureUpdateCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureUpdateCarCommandHandler> _logger;
        private readonly IValidator<FeatureUpdateCarCommandRequest> _validator;
        private readonly IMapper _mapper;

        public FeatureUpdateCarCommandHandler(
            IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureUpdateCarCommandHandler> logger,
            IValidator<FeatureUpdateCarCommandRequest> validator,
            IMapper mapper)
        {
            _featureReadRepository = featureReadRepository;
            _featureWriteRepository = featureWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<FeatureUpdateCarCommandResponse> Handle(FeatureUpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new FeatureUpdateCarCommandResponse();
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);

            if (!isExists)
            {
                return new FeatureUpdateCarCommandResponse();
            }

            var efEntity = _mapper.Map<EfEntity.Feature>(request);

            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var featureUpdatedCarEvent = _mapper.Map<FeatureUpdatedEvent>(request);

            featureUpdatedCarEvent.CreatedDate = DateTime.Now;

            try
            {
                mongoSession.StartTransaction();

                await _featureWriteRepository.UpdateAsync(efEntity);
                await _featureWriteRepository.SaveChangesAsync();

                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    CarEventType = CarEventType.CarFeatureUpdatedEvent,
                    Id = Guid.NewGuid(),
                    IsPublished = false,
                    Payload = featureUpdatedCarEvent.Serialize()!,
                    PublishDate = null,
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

            return null;
        }
    }
}
