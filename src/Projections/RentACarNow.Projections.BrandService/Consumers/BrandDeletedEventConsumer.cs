﻿using AutoMapper;
using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Helpers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.BrandService.Consumers
{
    public class BrandDeletedEventConsumer : BackgroundService
    {
        private readonly IBrandInboxRepository _brandInboxRepository;
        private readonly ILogger<BrandDeletedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IDateService _dateService;

        public BrandDeletedEventConsumer(
            IBrandInboxRepository brandInboxRepository,
            ILogger<BrandDeletedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IDateService dateTimeService)
        {
            _brandInboxRepository = brandInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _dateService = dateTimeService;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(BrandDeletedEventConsumer)} execute method has been  executed");


            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.BRAND_DELETED_QUEUE,
               consumeOperations: async (message) =>
               {
                   _logger.LogInformation(message);

                   var @event = message.Deseralize<BrandDeletedEvent>();

                   var foundedInboxMessage = await _brandInboxRepository.GetMessageByIdAsync(@event.MessageId);


                   if (foundedInboxMessage is not null) return;

                   var inboxMessage = new BrandInboxMessage
                   {
                       MessageId = @event.MessageId,
                       AddedDate = _dateService.GetDate(),
                       EventType = BrandEventType.BrandDeletedEvent,
                       Payload = message
                   };

                   await _brandInboxRepository.AddMessageAsync(inboxMessage);


               });


        }
    }

}
