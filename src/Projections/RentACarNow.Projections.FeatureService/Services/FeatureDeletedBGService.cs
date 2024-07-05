using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Feature;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.FeatureService.Services
{
    public class FeatureDeletedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoFeatureWriteRepository _featureWriteRepository;
        private readonly ILogger<FeatureDeletedBGService> _logger;

        public FeatureDeletedBGService(IRabbitMQMessageService messageService, IMongoFeatureWriteRepository featureWriteRepository, ILogger<FeatureDeletedBGService> logger)
        {
            _messageService = messageService;
            _featureWriteRepository = featureWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.FEATURE_DELETED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<FeatureDeletedEvent>();

                    await _featureWriteRepository.DeleteByIdAsync(@event.Id);

                });



            return Task.CompletedTask;
        }
    }
}
