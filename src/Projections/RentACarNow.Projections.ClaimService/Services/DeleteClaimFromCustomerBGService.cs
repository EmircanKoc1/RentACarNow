using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class DeleteClaimFromCustomerBGService : BackgroundService
    {
        private readonly IMongoClaimWriteRepository _writeRepository;
        private readonly ILogger<DeleteClaimFromCustomerBGService> _logger;
        private readonly IRabbitMQMessageService _messageService;

        public DeleteClaimFromCustomerBGService(IMongoClaimWriteRepository writeRepository, ILogger<DeleteClaimFromCustomerBGService> logger, IRabbitMQMessageService messageService)
        {
            _writeRepository = writeRepository;
            _logger = logger;
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue();




            return Task.CompletedTask;



        }
    }
}
