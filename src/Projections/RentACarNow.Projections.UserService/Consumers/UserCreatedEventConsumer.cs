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
    public class UserCreatedEventConsumer : BackgroundService
    {
        private readonly IUserInboxRepository _userInboxRepository;
        private readonly ILogger<UserCreatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public UserCreatedEventConsumer(
            IUserInboxRepository userInboxRepository,
            ILogger<UserCreatedEventConsumer> logger,
            IRabbitMQMessageService messageService)
        {
            _userInboxRepository = userInboxRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(UserCreatedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.USER_CREATED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<UserCreatedEvent>();

                    var foundedInboxMessage = await _userInboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new UserInboxMessage
                    {
                        AddedDate = DateHelper.GetDate(),
                        EventType = UserEventType.UserCreatedEvent,
                        MessageId = @event.MessageId,
                        Payload = message
                    };

                    await _userInboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
