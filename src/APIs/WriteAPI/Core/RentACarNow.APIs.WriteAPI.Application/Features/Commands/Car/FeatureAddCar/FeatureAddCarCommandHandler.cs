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
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureAddCar
{
    public class FeatureAddCarCommandHandler : IRequestHandler<FeatureAddCarCommandRequest, FeatureAddCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureAddCarCommandHandler> _logger;
        private readonly IValidator<FeatureAddCarCommandRequest> _validator;
        private readonly IMapper _mapper;

        public FeatureAddCarCommandHandler(IEfCoreFeatureReadRepository featureReadRepository, IEfCoreFeatureWriteRepository featureWriteRepository, IEfCoreCarReadRepository carReadRepository, ICarOutboxRepository carOutboxRepository, ILogger<FeatureAddCarCommandHandler> logger, IValidator<FeatureAddCarCommandRequest> validator, IMapper mapper)
        {
            _featureReadRepository = featureReadRepository;
            _featureWriteRepository = featureWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<FeatureAddCarCommandResponse> Handle(FeatureAddCarCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new FeatureAddCarCommandResponse();
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);

            if (!isExists)
            {
                return new FeatureAddCarCommandResponse();
            }

            var efEntity = _mapper.Map<EfEntity.Feature>(request);

            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();

            var featureAddedCarEvent = _mapper.Map<FeatureAddedCarEvent>(request);

            featureAddedCarEvent.CreatedDate = DateTime.Now;

            try
            {
                mongoSession.StartTransaction();

                await _featureWriteRepository.AddAsync(efEntity);
                await _featureWriteRepository.SaveChangesAsync();

                await _carOutboxRepository.AddMessageAsync(new CarOutboxMessage
                {
                    AddedDate = DateTime.Now,
                    CarEventType = CarEventType.CarFeatureAddedEvent,
                    Id = Guid.NewGuid(),
                    IsPublished = false,
                    Payload = featureAddedCarEvent.Serialize()!,
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
