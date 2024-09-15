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
    public class ClaimDeletedEventConsumer : BackgroundService
    {
        private readonly IClaimnboxRepository _claimInboxRepository;
        private readonly ILogger<ClaimDeletedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;

        public ClaimDeletedEventConsumer(
            IClaimnboxRepository claimInboxRepository,
            ILogger<ClaimDeletedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateService)
        {
            _claimInboxRepository = claimInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateService = dateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ClaimDeletedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_DELETED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<ClaimDeletedEvent>();

                    var foundedInboxMessage = await _claimInboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new ClaimInboxMessage
                    {
                        MessageId = @event.MessageId,
                        AddedDate = _dateService.GetDate(),
                        EventType = ClaimEventType.ClaimDeletedEvent,
                        Payload = message
                    };

                    await _claimInboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
