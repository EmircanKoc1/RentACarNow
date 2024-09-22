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

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.FeatureDeleteCar
{
    public class FeatureDeleteCarCommandRequestHandler : IRequestHandler<FeatureDeleteCarCommandRequest, FeatureDeleteCarCommandResponse>
    {
        private readonly IEfCoreFeatureReadRepository _featureReadRepository;
        private readonly IEfCoreFeatureWriteRepository _featureWriteRepository;
        private readonly IEfCoreCarReadRepository _carReadRepository;
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly ILogger<FeatureDeleteCarCommandRequestHandler> _logger;
        private readonly IValidator<FeatureDeleteCarCommandRequest> _validator;
        private readonly ICarEventFactory _carEventFactory;
        private readonly IDateService _dateService;
        private readonly IGuidService _guidService;

        public FeatureDeleteCarCommandRequestHandler(
            IEfCoreFeatureReadRepository featureReadRepository,
            IEfCoreFeatureWriteRepository featureWriteRepository,
            IEfCoreCarReadRepository carReadRepository,
            ICarOutboxRepository carOutboxRepository,
            ILogger<FeatureDeleteCarCommandRequestHandler> logger,
            IValidator<FeatureDeleteCarCommandRequest> validator,
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
            _carEventFactory = carEventFactory;
            _dateService = dateService;
            _guidService = guidService;
        }

        public async Task<FeatureDeleteCarCommandResponse> Handle(FeatureDeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(FeatureDeleteCarCommandRequestHandler)} Handle method has been executed");

            var validationResult = await _validator.ValidateAsync(request);



            if (!validationResult.IsValid)
            {
                _logger.LogInformation($"{nameof(FeatureDeleteCarCommandRequestHandler)} Request not validated");


                return new FeatureDeleteCarCommandResponse
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
                _logger.LogInformation($"{nameof(FeatureDeleteCarCommandRequestHandler)} feature not found , id : {request.FeatureId}");
                return new FeatureDeleteCarCommandResponse
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new List<ResponseErrorModel>(capacity: 1)
                    {
                        new ResponseErrorModel
                        {
                            ErrorMessage = "feature not found",
                            PropertyName = null
                        }
                    }
                };
            }

            var generatedMessageId = _guidService.CreateGuid();
            var generatedMessageAddedDate = _dateService.GetDate();

            var carFeatureDeletedEvent = _carEventFactory.CreateCarFeatureDeletedEvent(
                carId: foundedFeature.CarId,
                featureId: foundedFeature.Id).SetMessageId<CarFeatureDeletedEvent>(generatedMessageId);


            using var mongoSession = await _carOutboxRepository.StartSessionAsync();
            using var efTransaction = await _featureWriteRepository.BeginTransactionAsync();


            try
            {
                mongoSession.StartTransaction();

                _featureWriteRepository.DeleteById(request.FeatureId);
                await _featureWriteRepository.SaveChangesAsync();


                var outboxMessage = new CarOutboxMessage
                {
                    Id = generatedMessageId,
                    AddedDate = generatedMessageAddedDate,
                    CarEventType = CarEventType.CarFeatureDeletedEvent,
                    Payload = carFeatureDeletedEvent.Serialize()!
                };


                await _carOutboxRepository.AddMessageAsync(outboxMessage, mongoSession);

                await efTransaction.CommitAsync();
                await mongoSession.CommitTransactionAsync();

                _logger.LogInformation($"{nameof(FeatureDeleteCarCommandRequestHandler)} Transaction commited");
            }
            catch (Exception)
            {
                await efTransaction.RollbackAsync();
                await mongoSession.AbortTransactionAsync();

                _logger.LogError($"{nameof(FeatureDeleteCarCommandRequestHandler)} transaction rollbacked");

                return new FeatureDeleteCarCommandResponse
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

            return new FeatureDeleteCarCommandResponse
            {
                HttpStatusCode = HttpStatusCode.OK,
                Errors = null
            };
        }
    }
}
