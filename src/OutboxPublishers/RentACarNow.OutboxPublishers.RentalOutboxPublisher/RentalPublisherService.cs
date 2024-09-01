using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Infrastructure.Extensions;

namespace RentACarNow.OutboxPublishers.RentalOutboxPublisher
{
    public class RentalPublisherService : BackgroundService
    {
        private readonly IRabbitMQMessageService _rabbitMQMessageService;
        private readonly ILogger<RentalPublisherService> _logger;
        private readonly IRentalOutboxRepository _rentalOutboxRepository;

        public RentalPublisherService(
            IRabbitMQMessageService rabbitMQMessageService,
            ILogger<RentalPublisherService> logger,
            IRentalOutboxRepository rentalOutboxRepository)
        {
            _rabbitMQMessageService = rabbitMQMessageService;
            _logger = logger;
            _rentalOutboxRepository = rentalOutboxRepository;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(RentalPublisherService)} is executed ");

            while (!stoppingToken.IsCancellationRequested)
            {

                var outboxMessages = await _rentalOutboxRepository.GetOutboxMessagesAsync(5, OrderedDirection.None);


                foreach (var message in outboxMessages)
                {

                    var messagePayload = message.Payload;
                    var date = DateTime.Now;

                    switch (message.EventType)
                    {
                        case RentalEventType.RentalCreatedEvent:


                            var rentalCreatedEvent = messagePayload.Deseralize<RentalCreatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<RentalCreatedEvent>(
                                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.RENTAL_ADDED_ROUTING_KEY,
                                @event: rentalCreatedEvent);

                            await _rentalOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case RentalEventType.RentalUpdatedEvent:

                            var rentalUpdateEvent = messagePayload.Deseralize<RentalUpdatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<RentalUpdatedEvent>(
                                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.RENTAL_UPDATED_ROUTING_KEY,
                                @event: rentalUpdateEvent);

                            await _rentalOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case RentalEventType.RentalDeletedEvent:

                            var rentalDeletedEvent = messagePayload.Deseralize<RentalDeletedEvent>();

                            _rabbitMQMessageService.SendEventQueue<RentalDeletedEvent>(
                                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.RENTAL_DELETED_ROUTING_KEY,
                                @event: rentalDeletedEvent);

                            await _rentalOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        default:
                            _logger.LogInformation($"\"The event type didn't match any event\" , id : {message.Id} , event type : {message.EventType}");
                            break;
                    }
                    _logger.LogInformation(messagePayload);



                }

            }

        }
    }
}