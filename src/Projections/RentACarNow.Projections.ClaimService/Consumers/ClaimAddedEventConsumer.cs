
using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim; 
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces;
using RentACarNow.Common.MongoEntities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace RentACarNow.Projections.ClaimService.Consumers
{
    public class ClaimAddedEventConsumer : BackgroundService
    {
        private readonly IClaimnboxRepository _claimInboxRepository;
        private readonly ILogger<ClaimAddedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public ClaimAddedEventConsumer(
            IClaimnboxRepository claimInboxRepository,
            ILogger<ClaimAddedEventConsumer> logger,
            IRabbitMQMessageService messageService)
        {
            _claimInboxRepository = claimInboxRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ClaimAddedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_ADDED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<ClaimCreatedEvent>();

                    var foundedInboxMessage = await _claimInboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new ClaimInboxMessage
                    {
                        AddedDate = DateHelper.GetDate(),
                        EventType = ClaimEventType.ClaimAddedEvent,
                        MessageId = @event.MessageId,
                        Payload = message
                    };

                    await _claimInboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
