using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.OutboxPublishers.CarOutboxPublisher
{
    public class CarPublisherService : BackgroundService
    {
        private readonly ICarOutboxRepository _carOutboxRepository;
        private readonly IRabbitMQMessageService _rabbitMQMessageService;
        private readonly ILogger<CarPublisherService> _logger;

        public CarPublisherService(
            ICarOutboxRepository carOutboxRepository,
            IRabbitMQMessageService rabbitMQMessageService,
            ILogger<CarPublisherService> logger)
        {
            _carOutboxRepository = carOutboxRepository;
            _rabbitMQMessageService = rabbitMQMessageService;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("CarPublisherService Executed ");

            while (!stoppingToken.IsCancellationRequested)
            {

                var outboxMessages = await _carOutboxRepository.GetOutboxMessagesAsync(5, OrderedDirection.None);


                foreach (var message in outboxMessages)
                {


                    var messagePayload = message.Payload;
                    var date = DateHelper.GetDate();

                    switch (message.CarEventType)
                    {
                        case CarEventType.CarCreatedEvent:

                            var carCreatedEvent = messagePayload.Deseralize<CarCreatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<CarCreatedEvent>(
                                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.CAR_ADDED_ROUTING_KEY,
                                @event: carCreatedEvent);

                            await _carOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;

                        case CarEventType.CarDeletedEvent:

                            var carDeletedEvent = messagePayload.Deseralize<CarDeletedEvent>();

                            _rabbitMQMessageService.SendEventQueue<CarDeletedEvent>(
                                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.CAR_DELETED_ROUTING_KEY,
                                @event: carDeletedEvent);


                            await _carOutboxRepository.MarkPublishedMessageAsync(message.Id, date);
                            break;
                        case CarEventType.CarUpdatedEvent:


                            var carUpdatedEvent = messagePayload.Deseralize<CarUpdatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<CarUpdatedEvent>(
                                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.CAR_DELETED_ROUTING_KEY,
                                @event: carUpdatedEvent);

                            await _carOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case CarEventType.CarFeatureAddedEvent:

                            var carFeatureAddedEvent = messagePayload.Deseralize<CarFeatureAddedEvent>();

                            _rabbitMQMessageService.SendEventQueue<CarFeatureAddedEvent>(
                                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.CAR_FEATURE_ADDED_ROUTING_KEY,
                                @event: carFeatureAddedEvent);


                            await _carOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case CarEventType.CarFeatureDeletedEvent:

                            var carFeatureDeletedEvent = messagePayload.Deseralize<CarFeatureDeletedEvent>();

                            _rabbitMQMessageService.SendEventQueue<CarFeatureDeletedEvent>(
                                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.CAR_FEATURE_DELETED_ROUTING_KEY,
                                @event: carFeatureDeletedEvent);

                            await _carOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case CarEventType.CarFeatureUpdatedEvent:

                            var carFeatureUpdatedEvent = messagePayload.Deseralize<CarFeatureUpdatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<CarFeatureUpdatedEvent>(
                                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.CAR_FEATURE_UPDATED_ROUTING_KEY,
                                @event: carFeatureUpdatedEvent);

                            await _carOutboxRepository.MarkPublishedMessageAsync(message.Id, date);
                            break;
                        default:
                            _logger.LogInformation($"\"The event type didn't match any event\" , id : {message.Id} , event type : {message.CarEventType}");
                            break;
                    }
                    _logger.LogInformation(messagePayload);



                }


            }



        }
    }
}
