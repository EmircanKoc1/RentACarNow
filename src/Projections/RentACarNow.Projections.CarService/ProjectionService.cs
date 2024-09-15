using AutoMapper;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.CarService
{
    public class ProjectionService : BackgroundService
    {
        private readonly ICarInboxRepository _carInboxRepository;
        private readonly ILogger<ProjectionService> _logger;
        private readonly IMongoCarWriteRepository _carWriteRepository;
        private readonly IMapper _mapper;
        private readonly IDateService _dateService;

        public ProjectionService(
            ICarInboxRepository inboxRepository,
            ILogger<ProjectionService> logger,
            IMongoCarWriteRepository carWriteRepository,
            IMapper mapper,
            IDateService dateService)
        {
            _carInboxRepository = inboxRepository;
            _logger = logger;
            _carWriteRepository = carWriteRepository;
            _mapper = mapper;
            _dateService = dateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ProjectionService)} execute method has been executed ");

            while (!stoppingToken.IsCancellationRequested)
            {
                var inboxMessages = await _carInboxRepository
                   .GetMessagesAsync(5, OrderedDirection.None);

                foreach (var inboxMessage in inboxMessages)
                {
                    _logger.LogDebug($"{nameof(ProjectionService)} inbox message payload : {inboxMessage.Payload}");

                    var messagePayload = inboxMessage.Payload;
                    var date = _dateService.GetDate() ;

                    switch (inboxMessage.EventType)
                    {
                        case CarEventType.CarCreatedEvent:
                            var carCreatedEvent = messagePayload.Deseralize<CarCreatedEvent>();
                            await _carWriteRepository.AddAsync(_mapper.Map<Car>(carCreatedEvent));
                            await _carInboxRepository.MarkMessageProccessedAsync(carCreatedEvent.MessageId, date);
                            break;

                        case CarEventType.CarDeletedEvent:
                            var carDeletedEvent = messagePayload.Deseralize<CarDeletedEvent>();
                            await _carWriteRepository.DeleteByIdAsync(carDeletedEvent.CarId);
                            await _carInboxRepository.MarkMessageProccessedAsync(carDeletedEvent.MessageId, date);
                            break;

                        case CarEventType.CarUpdatedEvent:
                            var carUpdatedEvent = messagePayload.Deseralize<CarUpdatedEvent>();
                            await _carWriteRepository.UpdateAsync(_mapper.Map<Car>(carUpdatedEvent));
                            await _carInboxRepository.MarkMessageProccessedAsync(carUpdatedEvent.MessageId, date);
                            break;

                        case CarEventType.CarFeatureAddedEvent:
                            var carFeatureAddedEvent = messagePayload.Deseralize<CarFeatureAddedEvent>();

                            var feature = new Feature
                            {
                                CarId = carFeatureAddedEvent.CarId,
                                Id = carFeatureAddedEvent.FeatureId,
                                Name = carFeatureAddedEvent.Name,
                                Value = carFeatureAddedEvent.Value,
                            };

                            await _carWriteRepository.AddFeatureCarAsync(carFeatureAddedEvent.CarId, feature);
                            await _carInboxRepository.MarkMessageProccessedAsync(carFeatureAddedEvent.MessageId, date);



                            break;

                        case CarEventType.CarFeatureDeletedEvent:
                            var carFeatureDeletedEvent = messagePayload.Deseralize<CarFeatureDeletedEvent>();

                            await _carWriteRepository.DeleteFeatureCarAsync(carFeatureDeletedEvent.CarId, carFeatureDeletedEvent.FeatureId);

                            await _carInboxRepository.MarkMessageProccessedAsync(carFeatureDeletedEvent.MessageId, date);

                            break;

                        case CarEventType.CarFeatureUpdatedEvent:
                            var carFeatureUpdatedEvent = messagePayload.Deseralize<CarFeatureUpdatedEvent>();



                            await _carWriteRepository.UpateFeatureCarAsync(carFeatureUpdatedEvent.CarId, new Feature
                            {
                                CarId = carFeatureUpdatedEvent.CarId,
                                Id = carFeatureUpdatedEvent.FeatureId,
                                Name = carFeatureUpdatedEvent.Name,
                                Value = carFeatureUpdatedEvent.Value
                            });

                            await _carInboxRepository.MarkMessageProccessedAsync(carFeatureUpdatedEvent.MessageId, date);

                            break;

                        default:
                            _logger.LogWarning($"{nameof(ProjectionService)} CarEventType did not match any event");
                            break;
                    }
                }
            }
        }
    }
}
