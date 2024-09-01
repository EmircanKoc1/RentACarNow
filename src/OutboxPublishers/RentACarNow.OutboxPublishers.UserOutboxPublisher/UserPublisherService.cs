using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Events.User;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Infrastructure.Extensions;


namespace RentACarNow.OutboxPublishers.UserOutboxPublisher
{
    public class UserPublisherService : BackgroundService
    {
        private readonly IRabbitMQMessageService _rabbitMQMessageService;
        private readonly ILogger<UserPublisherService> _logger;
        private readonly IUserOutboxRepository _userOutboxRepository;

        public UserPublisherService(IRabbitMQMessageService rabbitMQMessageService, ILogger<UserPublisherService> logger, IUserOutboxRepository userOutboxRepository)
        {
            _rabbitMQMessageService = rabbitMQMessageService;
            _logger = logger;
            _userOutboxRepository = userOutboxRepository;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation($"{nameof(UserPublisherService)} is executed ");

            while (!stoppingToken.IsCancellationRequested)
            {

                var outboxMessages = await _userOutboxRepository.GetOutboxMessagesAsync(5, OrderedDirection.None);


                foreach (var message in outboxMessages)
                {

                    var messagePayload = message.Payload;
                    var date = DateTime.Now;


                    switch (message.EventType)
                    {
                        case UserEventType.UserCreatedEvent:

                            var userCreatedEvent = messagePayload.Deseralize<UserCreatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<UserCreatedEvent>(
                            exchangeName: RabbitMQExchanges.,
                                routingKey: RabbitMQRoutingKeys.USER_CREATED_ROUTING_KEY,
                                @event: userCreatedEvent);

                            await _userOutboxRepository.MarkPublishedMessageAsync(message.Id, date);

                            break;
                        case UserEventType.UserDeletedEvent:

                            var userDeletedEvent = messagePayload.Deseralize<UserDeletedEvent>();

                            _rabbitMQMessageService.SendEventQueue<UserDeletedEvent>(
                            exchangeName: RabbitMQExchanges.USER_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.USER_DELETED_ROUTING_KEY,
                                @event: userDeletedEvent);

                            await _userOutboxRepository.MarkPublishedMessageAsync(message.Id, date);



                            break;
                        case UserEventType.UserUpdatedEvent:

                            var userUpdatedEvent = messagePayload.Deseralize<UserUpdatedEvent>();

                            _rabbitMQMessageService.SendEventQueue<UserUpdatedEvent>(
                            exchangeName: RabbitMQExchanges.USER_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.USER_UPDATED_ROUTING_KEY,
                                @event: userUpdatedEvent);

                            await _userOutboxRepository.MarkPublishedMessageAsync(message.Id, date);


                            break;
                        case UserEventType.UserPasswordChangedEvent:

                            break;
                        case UserEventType.UserClaimAddedEvent:

                            var userClaimAddedEvent = messagePayload.Deseralize<UserClaimAddedEvent>();

                            _rabbitMQMessageService.SendEventQueue<UserClaimAddedEvent>(
                            exchangeName: RabbitMQExchanges.USER_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.USER_CLAIM_ADDED_ROUTING_KEY,
                                @event: userClaimAddedEvent);

                            await _userOutboxRepository.MarkPublishedMessageAsync(message.Id, date);


                            break;
                        case UserEventType.UserClaimDeletedEvent:

                            var userClaimDeletedEvent = messagePayload.Deseralize<UserClaimDeletedEvent>();

                            _rabbitMQMessageService.SendEventQueue<UserClaimDeletedEvent>(
                            exchangeName: RabbitMQExchanges.USER_EXCHANGE,
                                routingKey: RabbitMQRoutingKeys.USER_CLAIM_ADDED_ROUTING_KEY,
                                @event: userClaimDeletedEvent);

                            await _userOutboxRepository.MarkPublishedMessageAsync(message.Id, date);


                            break;
                        case UserEventType.UserClaimUpdatedEvent:
                            break;
                        default:
                            break;
                    }



                }

            }
        }
    }
}
