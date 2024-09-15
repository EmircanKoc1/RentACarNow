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
        private readonly IDateService _dateService;

        public UserClaimDeletedEventConsumer(
            IUserInboxRepository inboxRepository,
            ILogger<UserClaimDeletedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateService)
        {
            _inboxRepository = inboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateService = dateService;
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
                        MessageId = @event.MessageId,
                        AddedDate = _dateService.GetDate(),
                        EventType = UserEventType.UserClaimDeletedEvent,
                        Payload = message
                    };

                    await _inboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
