using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.UserClaimService.Consumers
{
    public class UserClaimUpdatedEventConsumer : BackgroundService
    {
        private readonly IUserInboxRepository _inboxRepository;
        private readonly ILogger<UserClaimUpdatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public UserClaimUpdatedEventConsumer(
            IUserInboxRepository inboxRepository,
            ILogger<UserClaimUpdatedEventConsumer> logger,
            IRabbitMQMessageService messageService)
        {
            _inboxRepository = inboxRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(UserClaimDeletedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.USER_CLAIM_UPDATED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<ClaimUpdatedEvent>();

                    var userClaimUpdatedEvent = new UserClaimUpdatedEvent
                    {
                        ClaimId = @event.Id,
                        Key = @event.Key,
                        Value = @event.Value
                    };


                    var foundedInboxMessage = await _inboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new UserInboxMessage
                    {
                        AddedDate = DateHelper.GetDate(),
                        EventType = UserEventType.UserClaimUpdatedEvent,
                        MessageId = @event.MessageId,
                        Payload = userClaimUpdatedEvent.Serialize()!
                    };

                    await _inboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
