using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class DeleteClaimFromEmployeeBGService : BackgroundService
    {
        private readonly IMongoClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromEmployeeBGService> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public DeleteClaimFromEmployeeBGService(IMongoClaimWriteRepository writeRepository, ILogger<DeleteClaimFromEmployeeBGService> logger, IRabbitMQMessageService messageService)
        {
            _writeRepository = writeRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_DELETED_FROM_EMPLOYEE_QUEUE,
                message =>
                {

                    var @event = message.Deseralize<ClaimDeletedFromEmployeeEvent>();


                    _writeRepository.DeleteClaimFromCustomerAsync(@event.ClaimId, @event.EmployeeId);

                });

            return Task.CompletedTask;
        }
    }
}
