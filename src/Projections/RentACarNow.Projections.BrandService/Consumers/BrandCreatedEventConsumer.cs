using AutoMapper;
using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;
using System.Diagnostics.Tracing;

namespace RentACarNow.Projections.BrandService.Consumers
{
    public class BrandCreatedEventConsumer : BackgroundService
    {
        private readonly IBrandInboxRepository _brandInboxRepository;
        private readonly ILogger<BrandCreatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateTimeService;

        public BrandCreatedEventConsumer(
            IBrandInboxRepository brandInboxRepository,
            ILogger<BrandCreatedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateTimeService)
        {
            _brandInboxRepository = brandInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateTimeService = dateTimeService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(BrandCreatedEventConsumer)} execute method has been  executed");


            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
               consumeOperations: async (message) =>
               {
                   _logger.LogInformation(message);

                   var @event = message.Deseralize<BrandCreatedEvent>();

                   var foundedInboxMessage = await _brandInboxRepository.GetMessageByIdAsync(@event.MessageId);


                   if (foundedInboxMessage is not null) return;


                   var inboxMessage = new BrandInboxMessage
                   {
                       MessageId = @event.MessageId,
                       AddedDate = _dateTimeService.GetDate(),
                       EventType = BrandEventType.BrandAddedEvent,
                       Payload = message
                   };

                  await _brandInboxRepository.AddMessageAsync(inboxMessage);

               });



        }

    }

}
