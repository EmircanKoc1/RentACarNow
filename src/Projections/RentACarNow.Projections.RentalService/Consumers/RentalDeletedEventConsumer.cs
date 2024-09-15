using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Rental;
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

namespace RentACarNow.Projections.RentalService.Consumers
{
    public class RentalDeletedEventConsumer : BackgroundService
    {
        private readonly IRentalInboxRepository _rentalInboxRepository;
        private readonly ILogger<RentalDeletedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;

        public RentalDeletedEventConsumer(
            IRentalInboxRepository rentalInboxRepository,
            ILogger<RentalDeletedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateService)
        {
            _rentalInboxRepository = rentalInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateService = dateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(RentalDeletedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.RENTAL_DELETED_QUEUE,
                consumeOperations: async (message) =>
                {
                    _logger.LogInformation(message);

                    var @event = message.Deseralize<RentalDeletedEvent>();

                    var foundedInboxMessage = await _rentalInboxRepository.GetMessageByIdAsync(@event.MessageId);

                    if (foundedInboxMessage is not null) return;

                    var inboxMessage = new RentalInboxMessage
                    {
                        MessageId = @event.MessageId,
                        AddedDate = _dateService.GetDate(),
                        EventType = RentalEventType.RentalDeletedEvent,
                        Payload = message
                    };

                    await _rentalInboxRepository.AddMessageAsync(inboxMessage);
                });
        }
    }
}
