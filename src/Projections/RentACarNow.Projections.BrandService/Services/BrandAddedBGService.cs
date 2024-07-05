using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.BrandService.Services
{
    public class BrandAddedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly MongoBrandWriteRepository _brandWriteRepository;
        private readonly ILogger<BrandAddedBGService> _logger;

        public BrandAddedBGService(IRabbitMQMessageService messageService, MongoBrandWriteRepository brandWriteRepository, ILogger<BrandAddedBGService> logger)
        {
            _messageService = messageService;
            _brandWriteRepository = brandWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
                async (message) =>
                {

                    var @event = message.Deseralize<BrandAddedEvent>();

                    await _brandWriteRepository.AddAsync(new Brand
                    {
                        

                        
                    });


                });

        }
    }
}
