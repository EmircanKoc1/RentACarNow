using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Feature;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.FeatureService.Services
{
    public class FeatureAddedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoFeatureWriteRepository _featureWriteRepository;
        private readonly ILogger<FeatureAddedBGService> _logger;

        public FeatureAddedBGService(IRabbitMQMessageService messageService, IMongoFeatureWriteRepository featureWriteRepository, ILogger<FeatureAddedBGService> logger )
        {
            _messageService = messageService;
            _featureWriteRepository = featureWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.FEATURE_ADDED_QUEUE,
                 message =>
                {
                    var @event = message.Deseralize<FeatureAddedEvent>();

                    _featureWriteRepository.AddFeatureByCarId(@event.CarId, new Feature
                    {
                        Id = @event.Id,
                        CarId = @event.CarId,
                        Name = @event.Name,
                        Value = @event.Value,
                        CreatedDate = @event.CreatedDate,
                        DeletedDate = @event.DeletedDate,
                        UpdatedDate = @event.UpdatedDate,
                    });


                });



            return Task.CompletedTask;
        }
    }
}
