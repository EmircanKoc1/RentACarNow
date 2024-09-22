using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Read.EfCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureUpdateCar
{

    public class FeatureUpdateCarCommandRequestHandler : IRequestHandler<FeatureUpdateCarCommandRequest, FeatureUpdateCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureUpdateCarCommandRequestHandler> _logger;
        private readonly IValidator<FeatureUpdateCarCommandRequest> _validator;
        private readonly IMapper _mapper;
        private readonly ICarEventFactory _carEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public FeatureUpdateCarCommandRequestHandler(
            IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureUpdateCarCommandRequestHandler> logger,
            IValidator<FeatureUpdateCarCommandRequest> validator,
            IMapper mapper,
            ICarEventFactory carEventFactory,
            IDateService dateService,
            IGuidService guidService)
        {
            _featureReadRepository = featureReadRepository;
            _featureWriteRepository = featureWriteRepository;
            _carReadRepository = carReadRepository;
            _carOutboxRepository = carOutboxRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _carEventFactory = carEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<FeatureUpdateCarCommandResponse> Handle(FeatureUpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(FeatureUpdateCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(FeatureUpdateCarCommandRequestHandler)} Request not validated");

                return new FeatureUpdateCarCommandResponse
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };

            }

            var foundedFeature = await _featureReadRepository.GetByIdAsync(request.FeatureId);


            if (foundedFeature is null)
            {
                _logger.LogInformation($"{nameof(FeatureUpdateCarCommandRequestHandler)} Entity not found , id : {request.FeatureId}");

                return new FeatureUpdateCarCommandResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "feature or car  not found",
                            PropertyName = null
                        }
}
                };


            }

            var generatedUpdatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();
            var generatedMessageId = _guidService.CreateGuid();




            var efEntity = _mapper.Map<EfEntity.Feature>(request);
            efEntity.UpdatedDate = generatedUpdatedDate;

            var carFeatureUpdatedEvent = _carEventFactory.CreateCarFeatureUpdatedEvent(
                carId: foundedFeature.CarId,
                featureId: foundedFeature.Id,
                name: request.Name,
                value: request.Value).SetMessageId<CarFeatureUpdatedEvent>(generatedMessageId);


            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();





            try
            {
                mongoSession.StartTransaction();

                await _featureWriteRepository.UpdateAsync(efEntity);
                await _featureWriteRepository.SaveChangesAsync();

                var outboxMessage = new CarOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    CarEventType = CarEventType.CarFeatureUpdatedEvent,
                    Payload = carFeatureUpdatedEvent.Serialize()! 
                };

                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(FeatureUpdateCarCommandRequestHandler)} transaction rollbacked");

                return new FeatureUpdateCarCommandResponse
                {

                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "Transaction exception",
                            PropertyName = null
                        }
                    }
                };
            }

            return new FeatureUpdateCarCommandResponse
            {
                HttpStatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
