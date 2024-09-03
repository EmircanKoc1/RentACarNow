using AutoMapper;
using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.BrandService.Consumers
{
    public class BrandUpdatedEventConsumer : BackgroundService
    {
        private readonly IBrandInboxRepository _brandInboxRepository;
        private readonly ILogger<BrandCreatedEventConsumer> _logger;
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoBrandWriteRepository _brandWriteRepository;
        private readonly IMapper _mapper;
        public BrandUpdatedEventConsumer(
            IBrandInboxRepository brandInboxRepository,
            ILogger<BrandCreatedEventConsumer> logger,
            IRabbitMQMessageService messageService,
            IMongoBrandWriteRepository brandWriteRepository,
            IMapper mapper)
        {
            _brandInboxRepository = brandInboxRepository;
            _logger = logger;
            _messageService = messageService;
            _brandWriteRepository = brandWriteRepository;
            _mapper = mapper;
        }



        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("executed");

            _messageService.ConsumeQueue(
              queueName: RabbitMQQueues.BRAND_UPDATED_QUEUE,
              consumeOperations: async (message) =>
              {
                  _logger.LogInformation(message);

                  var @event = message.Deseralize<BrandUpdatedEvent>();

                  var foundedInboxMessage = await _brandInboxRepository.GetMessageByIdAsync(@event.MessageId);


                  var brand = _mapper.Map<Brand>(@event);

                  if (foundedInboxMessage is not null)
                  {

                      await _brandWriteRepository.UpdateAsync(brand);

                      await _brandInboxRepository.MarkMessageProccessedAsync(
                          id: foundedInboxMessage.MessageId,
                          proccessedDate: DateTime.Now);

                      return;
                  }

                  await _brandInboxRepository.AddMessageAsync(new BrandInboxMessage
                  {
                      MessageId = @event.MessageId,
                      AddedDate = DateTime.Now,
                      Payload = message
                  });

                  await _brandWriteRepository.UpdateAsync(brand);

                  await _brandInboxRepository.MarkMessageProccessedAsync(@event.MessageId, DateTime.Now);

              });
        }
    }

}
