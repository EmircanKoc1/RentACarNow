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
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Models;
using System.Net;
using EfEntity = RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureAddCar
{
    public class FeatureAddCarCommandRequestHandler : IRequestHandler<FeatureAddCarCommandRequest, FeatureAddCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureAddCarCommandRequestHandler> _logger;
        private readonly IValidator<FeatureAddCarCommandRequest> _validator;
        private readonly IMapper _mapper;
        private readonly ICarEventFactory _carEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public FeatureAddCarCommandRequestHandler(
            IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureAddCarCommandRequestHandler> logger,
            IValidator<FeatureAddCarCommandRequest> validator,
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

        public async Task<FeatureAddCarCommandResponse> Handle(FeatureAddCarCommandRequest request, CancellationToken cancellationToken)
        {

            _logger.LogDebug($"{nameof(FeatureAddCarCommandRequestHandler)} Handle method has been executed");


            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(FeatureAddCarCommandRequestHandler)} Request not validated");


                return new FeatureAddCarCommandResponse
                {
                    CarId = request.CarId,
                    FeatureId = _guidService.GetEmptyGuid(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors?.Select(vf => new ResponseErrorModel
                    {
                        PropertyName = vf.PropertyName,
                        ErrorMessage = vf.ErrorMessage
                    })
                };
            }

            var isExists = await _carReadRepository.IsExistsAsync(request.CarId);


            if (!isExists)
            {
                _logger.LogInformation($"{nameof(FeatureAddCarCommandRequestHandler)} car not found , id : {request.CarId}");
                return new FeatureAddCarCommandResponse
                {
                    CarId = request.CarId,
                    FeatureId = _guidService.GetEmptyGuid(),
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "car not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedFeatureId = _guidService.GetEmptyGuid();
            var generatedMessageId = _guidService.GetEmptyGuid();
            var generatedCreatedDate = _dateService.GetDate();
            var generatedMessageAddedDate = _dateService.GetDate();


            var efEntity = _mapper.Map<EfEntity.Feature>(request);
            efEntity.Id = generatedFeatureId;
            efEntity.CreatedDate = generatedCreatedDate;



            var carFeatureAddedEvent = _carEventFactory.CreateCarFeatureAddedEvent(
                carId: request.CarId,
                featureId: generatedFeatureId,
                name: request.Name,
                value: request.Value).SetMessageId<CarFeatureAddedEvent>(generatedMessageId);






            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();
            using var mongoSession = await _carOutboxRepository.StartSessionAsync();


            try
            {
                mongoSession.StartTransaction();

                await _featureWriteRepository.AddAsync(efEntity);
                await _featureWriteRepository.SaveChangesAsync();


                var outboxMessage = new CarOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    CarEventType = CarEventType.CarFeatureAddedEvent,
                    Payload = carFeatureAddedEvent.Serialize()!
                };

                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);



                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(FeatureAddCarCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(FeatureAddCarCommandRequestHandler)} transaction rollbacked");

                return new FeatureAddCarCommandResponse
                {
                    CarId = request.CarId,
                    FeatureId = _guidService.GetEmptyGuid(),
                    StatusCode = HttpStatusCode.BadRequest,
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


            return new FeatureAddCarCommandResponse
            {
                CarId = request.CarId,
                FeatureId = generatedFeatureId,
                StatusCode = HttpStatusCode.Created,
                Errors = null
            };
        }
    }
}
