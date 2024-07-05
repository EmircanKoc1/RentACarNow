using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class ClaimUpdatedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoClaimWriteRepository _claimWriteRepository;
        private readonly ILogger<ClaimUpdatedBGService> _logger;

        public ClaimUpdatedBGService(IRabbitMQMessageService messageService,
            IMongoClaimWriteRepository claimWriteRepository,
            ILogger<ClaimUpdatedBGService> logger)
        {
            _messageService = messageService;
            _claimWriteRepository = claimWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_UPDATED_QUEUE,
                async message =>
                {

                    var @event = message.Deseralize<ClaimUpdatedEvent>();

                    await _claimWriteRepository.UpdateAsync(new Claim
                    {
                        Id = @event.Id,
                        Key = @event.Key,
                        Value = @event.Value,
                        CreatedDate = @event.CreatedDate,
                        UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
                        DeletedDate = @event.DeletedDate
                    });

                });



            return Task.CompletedTask;
        }
    }
}
