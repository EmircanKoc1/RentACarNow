using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class ClaimDeletedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoClaimWriteRepository _claimWriteRepository;
        private readonly ILogger<ClaimDeletedBGService> _logger;

        public ClaimDeletedBGService(IRabbitMQMessageService messageService, IMongoClaimWriteRepository claimWriteRepository, ILogger<ClaimDeletedBGService> logger)
        {
            _messageService = messageService;
            _claimWriteRepository = claimWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {


            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_DELETED_QUEUE,
                async message =>
                {

                    var @event = message.Deseralize<ClaimDeletedEvent>();

                    await _claimWriteRepository.DeleteByIdAsync(@event.Id);

                });



            return Task.CompletedTask;
        }
    }
}
