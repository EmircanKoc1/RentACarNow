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

namespace RentACarNow.Projections.BrandService.Consumers
{
    public class BrandUpdatedEventConsumer : BackgroundService
    {
        private readonly IBrandInboxRepository _brandInboxRepository;
        private readonly ILogger<BrandUpdatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;
        public BrandUpdatedEventConsumer(
            IBrandInboxRepository brandInboxRepository,
            ILogger<BrandUpdatedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateService)
        {
            _brandInboxRepository = brandInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateService = dateService;
        }



        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(BrandUpdatedEventConsumer)} execute method has been  executed");


            _messageService.ConsumeQueue(
              queueName: RabbitMQQueues.BRAND_UPDATED_QUEUE,
              consumeOperations: async (message) =>
              {
                  _logger.LogInformation(message);

                  var @event = message.Deseralize<BrandUpdatedEvent>();

                  var foundedInboxMessage = await _brandInboxRepository.GetMessageByIdAsync(@event.MessageId);

                  if (foundedInboxMessage is not null) return;
                 

                  await _brandInboxRepository.AddMessageAsync(new BrandInboxMessage
                  {
                      MessageId = @event.MessageId,
                      AddedDate = _dateService.GetDate(),
                      EventType = BrandEventType.BrandUpdatedEvent,
                      Payload = message
                  });



              });
        }
    }

}
