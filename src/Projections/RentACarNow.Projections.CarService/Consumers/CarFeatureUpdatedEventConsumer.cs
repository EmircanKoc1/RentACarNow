using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACarNow.Common.Events.Car;

namespace RentACarNow.Projections.CarService.Consumers
{
    internal class CarFeatureUpdatedEventConsumer : BackgroundService
    {
        private readonly ICarInboxRepository _inboxRepository;
        private readonly ILogger<CarFeatureUpdatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;

        public CarFeatureUpdatedEventConsumer(
            ICarInboxRepository inboxRepository, 
            ILogger<CarFeatureUpdatedEventConsumer> logger,
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
            _logger.LogInformation($"{nameof(CarFeatureUpdatedEventConsumer)} execute method has been executed");

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.CAR_FEATURE_UPDATED_QUEUE,
               consumeOperations: async (message) =>
               {
                   _logger.LogInformation(message);

                   var @event = message.Deseralize<CarFeatureUpdatedEvent>();

                   var foundedInboxMessage = await _inboxRepository.GetMessageByIdAsync(@event.MessageId);

                   if (foundedInboxMessage is not null) return;

                   var inboxMessage = new CarInboxMessage
                   {
                       MessageId = @event.MessageId,
                       AddedDate = _dateService.GetDate(),
                       EventType = CarEventType.CarFeatureUpdatedEvent,
                       Payload = message
                   };

                   await _inboxRepository.AddMessageAsync(inboxMessage);
               });
        }
    }
}
