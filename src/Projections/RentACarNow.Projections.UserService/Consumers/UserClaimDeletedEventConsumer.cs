using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.UserClaimService.Consumers
{
    public class UserClaimDeletedEventConsumer : BackgroundService
    {
        private readonly IUserInboxRepository _inboxRepository;
        private readonly ILogger<UserClaimDeletedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public UserClaimDeletedEventConsumer(
            IUserInboxRepository inboxRepository,
            ILogger<UserClaimDeletedEventConsumer> logger,
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
                queueName: RabbitMQQueues.USER_CLAIM_DELETED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<UserClaimDeletedEvent>();

                    var foundedInboxMessage = await _inboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new UserInboxMessage
                    {
                        AddedDate = DateHelper.GetDate(),
                        EventType = UserEventType.UserClaimDeletedEvent,
                        MessageId = @event.MessageId,
                        Payload = message
                    };

                    await _inboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
