using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class DeleteClaimFromAdminBGService : BackgroundService
    {
        private readonly IMongoClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromAdminBGService> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public DeleteClaimFromAdminBGService(IMongoClaimWriteRepository writeRepository, ILogger<DeleteClaimFromAdminBGService> logger, IRabbitMQMessageService messageService)
        {
            _writeRepository = writeRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_DELETED_FROM_ADMIN_QUEUE,
                message =>
                {

                    var @event = message.Deseralize<ClaimDeletedFromAdminEvent>();


                    _writeRepository.DeleteClaimFromAdminAsync(@event.ClaimId, @event.AdminId);

                });

            return Task.CompletedTask;
        }
    }
}
