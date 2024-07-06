using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class ClaimAddedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoClaimWriteRepository _claimWriteRepository;
        private readonly ILogger<ClaimAddedBGService> _logger;

        public ClaimAddedBGService(IRabbitMQMessageService messageService,
            IMongoClaimWriteRepository claimWriteRepository, ILogger<ClaimAddedBGService> logger)
        {
            _messageService = messageService;
            _claimWriteRepository = claimWriteRepository;
            _logger = logger;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_ADDED_QUEUE,
                 message =>
                {
                    var @event = message.Deseralize<ClaimAddedEvent>();

                     _claimWriteRepository.AddAsync(new Claim
                    {
                        Key = @event.Key,
                        Value = @event.Value,
                        CreatedDate = @event.CreatedDate ?? DateTime.UtcNow,
                        UpdatedDate = @event.UpdatedDate,
                        DeletedDate = @event.DeletedDate

                    });


                });





            return Task.CompletedTask;
        }
    }
}
