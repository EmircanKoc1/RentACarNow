using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Infrastructure.Extensions;

namespace RentACarNow.OutboxPublishers.ClaimOutboxPublisher;

public class ClaimPublisherService : BackgroundService
{
    private readonly IRabbitMQMessageService _rabbitMQMessageService;
    private readonly ILogger<ClaimPublisherService> _logger;
    private readonly IClaimOutboxRepository _claimOutboxRepository;

    public ClaimPublisherService(IRabbitMQMessageService rabbitMQMessageService, ILogger<ClaimPublisherService> logger, IClaimOutboxRepository claimOutboxRepository)
    {
        _rabbitMQMessageService = rabbitMQMessageService;
        _logger = logger;
        _claimOutboxRepository = claimOutboxRepository;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        _logger.LogInformation($"{nameof(ClaimPublisherService)} is executed ");

        while (!stoppingToken.IsCancellationRequested)
        {

            var outboxMessages = await _claimOutboxRepository.GetOutboxMessagesAsync(8, OrderedDirection.None);


            foreach (var message in outboxMessages)
            {

                var messagePayload = message.Payload;
                var date = DateTime.Now;

                switch (message.ClaimEventType)
                {
                    case ClaimEventType.ClaimAddedEvent:

                        var claimCreatedEvent = messagePayload.Deseralize<ClaimAddedEvent>();

                        _rabbitMQMessageService.SendEventQueue<ClaimAddedEvent>(
                            exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                            routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_ROUTING_KEY,
                            @event: claimCreatedEvent);

                        await _claimOutboxRepository.MarkPublishedMessageAsync(message.Id, date);


                        break;
                    case ClaimEventType.ClaimDeletedEvent:

                        var claimDeletedEvent = messagePayload.Deseralize<ClaimDeletedEvent>();

                        _rabbitMQMessageService.SendEventQueue<ClaimDeletedEvent>(
                            exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                            routingKey: RabbitMQRoutingKeys.CLAIM_DELETED_ROUTING_KEY,
                            @event: claimDeletedEvent);

                        await _claimOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                        break;
                    case ClaimEventType.ClaimUpdatedEvent:

                        var claimUpdatedEvent = messagePayload.Deseralize<ClaimUpdatedEvent>();

                        _rabbitMQMessageService.SendEventQueue<ClaimUpdatedEvent>(
                            exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                            routingKey: RabbitMQRoutingKeys.CLAIM_UPDATED_ROUTING_KEY,
                            @event: claimUpdatedEvent);

                        await _claimOutboxRepository.MarkPublishedMessageAsync(message.Id, date);


                        break;
                    default:
                        _logger.LogInformation($"\"The event type didn't match any event\" , id : {message.Id} , event type : {message.ClaimEventType}");
                        break;
                }


            }
        }






    }
}
