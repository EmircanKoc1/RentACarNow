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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureDeleteCar
{
    public class FeatureDeleteCarCommandHandler : IRequestHandler<FeatureDeleteCarCommandRequest, FeatureDeleteCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureDeleteCarCommandHandler> _logger;
        private readonly IValidator<FeatureDeleteCarCommandRequest> _validator;
        private readonly IMapper _mapper;

        public FeatureDeleteCarCommandHandler(IEfCoreFeatureReadRepository featureReadRepository, 
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository, 
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureDeleteCarCommandHandler> logger,
            IValidator<FeatureDeleteCarCommandRequest> validator, 
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

        public async Task<FeatureDeleteCarCommandResponse> Handle(FeatureDeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new FeatureDeleteCarCommandResponse();
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);

            if (!isExists)
            {
                return new FeatureDeleteCarCommandResponse();
            }

            var efEntity = _mapper.Map<EfEntity.Feature>(request);

            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var featureDeletedCarEvent = _mapper.Map<FeatureDeletedEvent>(request);

            featureDeletedCarEvent.CreatedDate = DateTime.Now;

            try
            {
                mongoSession.StartTransaction();

                _featureWriteRepository.Delete(efEntity);
                await _featureWriteRepository.SaveChangesAsync();

                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    CarEventType = CarEventType.CarFeatureDeletedEvent,
                    Id = Guid.NewGuid(),
                    IsPublished = false,
                    Payload = featureDeletedCarEvent.Serialize()!,
                    PublishDate = null,
                }, mongoSession);

                efTransaction.Commit();
                await mongoSession.CommitTransactionAsync();
            }
            catch (Exception)
            {
                 efTransaction.Rollback();
                await mongoSession.AbortTransactionAsync();

                throw;
            }

            return null;
        }
    }
}
