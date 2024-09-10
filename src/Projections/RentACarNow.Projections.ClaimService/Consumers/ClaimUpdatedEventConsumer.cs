using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Consumers
{
    public class ClaimUpdatedEventConsumer : BackgroundService
    {
        private readonly IClaimnboxRepository _claimInboxRepository;
        private readonly ILogger<ClaimUpdatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public ClaimUpdatedEventConsumer(
            IClaimnboxRepository claimInboxRepository,
            ILogger<ClaimUpdatedEventConsumer> logger,
            IRabbitMQMessageService messageService)
        {
            _claimInboxRepository = claimInboxRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ClaimUpdatedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_UPDATED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<ClaimUpdatedEvent>();

                    var foundedInboxMessage = await _claimInboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new ClaimInboxMessage
                    {
                        AddedDate = DateHelper.GetDate(),
                        EventType = ClaimEventType.ClaimUpdatedEvent,
                        MessageId = @event.MessageId,
                        Payload = message
                    };

                    await _claimInboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
