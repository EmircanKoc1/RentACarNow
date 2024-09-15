using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.User;
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

namespace RentACarNow.Projections.UserService.Consumers
{
    public class UserUpdatedEventConsumer : BackgroundService
    {
        private readonly IUserInboxRepository _userInboxRepository;
        private readonly ILogger<UserUpdatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;

        public UserUpdatedEventConsumer(
            IUserInboxRepository userInboxRepository,
            ILogger<UserUpdatedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateService)
        {
            _userInboxRepository = userInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateService = dateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(UserUpdatedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.USER_UPDATED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<UserUpdatedEvent>();

                    var foundedInboxMessage = await _userInboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new UserInboxMessage
                    {
                        MessageId = @event.MessageId,
                        AddedDate = _dateService.GetDate(),
                        EventType = UserEventType.UserUpdatedEvent,
                        Payload = message
                    };

                    await _userInboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
