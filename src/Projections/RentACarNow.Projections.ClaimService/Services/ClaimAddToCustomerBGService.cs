using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class ClaimAddToCustomerBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoClaimWriteRepository _claimWriteRepository;
        private readonly ILogger<ClaimAddToCustomerBGService> _logger;


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_ADDED_TO_ADMIN_QUEUE,
                message =>
                {
                    var @event = message.Deseralize<ClaimAddedToAdminEvent>();



                });


            return Task.CompletedTask;
        }
    }

}
