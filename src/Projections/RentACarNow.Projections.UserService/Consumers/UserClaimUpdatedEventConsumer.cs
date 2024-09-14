using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
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
        private readonly IUserEventFactory _userEventFactory;
        private readonly IDateService _dateService;

        public UserClaimUpdatedEventConsumer(
            IUserInboxRepository inboxRepository,
            ILogger<UserClaimUpdatedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IUserEventFactory userEventFactory,
            IDateService dateService)
        {
            _inboxRepository = inboxRepository;
            _logger = logger;
            _messageService = messageService;
            _userEventFactory = userEventFactory;
            _dateService = dateService;
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

                    var userClaimUpdatedEvent = _userEventFactory.CreateUserClaimUpdatedEvent(
                        claimId: @event.ClaimId,
                        key: @event.Key,
                        value: @event.Value);


                    var foundedInboxMessage = await _inboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new UserInboxMessage
                    {
                        MessageId = @event.MessageId,
                        AddedDate = _dateService.GetDate(),
                        EventType = UserEventType.UserClaimUpdatedEvent,
                        Payload = userClaimUpdatedEvent.Serialize()!
                    };

                    await _inboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
