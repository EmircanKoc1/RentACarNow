using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
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
using RentACarNow.Common.Events.User;

namespace RentACarNow.Projections.UserClaimService.Consumers
{
    public class UserClaimAddedEventConsumer : BackgroundService
    {
        private readonly IUserInboxRepository _inboxRepository;
        private readonly ILogger<UserClaimAddedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;

        public UserClaimAddedEventConsumer(
            IUserInboxRepository inboxRepository,
            ILogger<UserClaimAddedEventConsumer> logger,
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
            _logger.LogInformation($"{nameof(UserClaimAddedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.USER_CLAIM_ADDED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<UserClaimAddedEvent>();

                    var foundedInboxMessage = await _inboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new UserInboxMessage
                    {
                        MessageId = @event.MessageId,
                        AddedDate = _dateService.GetDate(),
                        EventType = UserEventType.UserClaimAddedEvent,
                        Payload = message
                    };

                    await _inboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
