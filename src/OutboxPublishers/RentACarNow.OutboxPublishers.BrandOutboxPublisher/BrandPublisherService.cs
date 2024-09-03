
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.OutboxPublishers.BrandOutboxPublisher
{
    public class BrandPublisherService : BackgroundService
    {
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly IRabbitMQMessageService _rabbitMQMessageService;
        private readonly ILogger<BrandPublisherService> _logger;

        public BrandPublisherService(
            IBrandOutboxRepository brandOutboxRepository,
            IRabbitMQMessageService rabbitMQMessageService,
            ILogger<BrandPublisherService> logger)
        {
            _brandOutboxRepository = brandOutboxRepository;
            _rabbitMQMessageService = rabbitMQMessageService;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BrandPublisherService Executed ");

            while (!stoppingToken.IsCancellationRequested)
            {

                var outboxMessages = await _brandOutboxRepository.GetOutboxMessagesAsync(5, OrderedDirection.None);


                foreach (var message in outboxMessages)
                {

                    var messagePayload = message.Payload;
                    var date = DateTime.Now;

                    switch (message.EventType)
                    {
                        case BrandEventType.BrandAddedEvent:

                            var brandCreatedEvent = messagePayload.Deseralize<BrandCreatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<BrandCreatedEvent>(
                                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.BRAND_ADDED_ROUTING_KEY,
                                @event: brandCreatedEvent);

                            await _brandOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case BrandEventType.BrandDeletedEvent:
                            
                            var brandDeletedEvent = messagePayload.Deseralize<BrandDeletedEvent>();

                            _rabbitMQMessageService.SendEventQueue<BrandDeletedEvent>(
                                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.BRAND_DELETED_ROUTING_KEY,
                                @event: brandDeletedEvent);

                            await _brandOutboxRepository.MarkPublishedMessageAsync(message.Id, date);


                            break;
                        case BrandEventType.BrandUpdatedEvent:
                           
                            var brandUpdatedEvent = messagePayload.Deseralize<BrandUpdatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<BrandUpdatedEvent>(
                                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.BRAND_UPDATED_ROUTING_KEY,
                                @event: brandUpdatedEvent);

                            await _brandOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

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
