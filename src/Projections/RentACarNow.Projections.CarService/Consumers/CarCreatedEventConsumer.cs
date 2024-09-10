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
    internal class CarCreatedEventConsumer : BackgroundService
    {
        private readonly ICarInboxRepository _inboxRepository;
        private readonly ILogger<CarCreatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public CarCreatedEventConsumer(ICarInboxRepository inboxRepository, ILogger<CarCreatedEventConsumer> logger, IRabbitMQMessageService messageService)
        {
            _inboxRepository = inboxRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(CarCreatedEventConsumer)} execute method has been  executed");


            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
               consumeOperations: async (message) =>
               {
                   _logger.LogInformation(message);

                   var @event = message.Deseralize<CarCreatedEvent>();

                   var foundedInboxMessage = await _inboxRepository.GetMessageByIdAsync(@event.MessageId);


                   if (foundedInboxMessage is not null) return;


                   var inboxMessage = new CarInboxMessage
                   {
                       AddedDate = DateHelper.GetDate(),
                       EventType = CarEventType.CarCreatedEvent,
                       MessageId = @event.MessageId,
                       Payload = message
                   };

                   await _inboxRepository.AddMessageAsync(inboxMessage);

               });
        }
    }
}
