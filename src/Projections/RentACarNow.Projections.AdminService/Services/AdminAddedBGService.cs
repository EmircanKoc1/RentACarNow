using RentACarNow.Common.Constants.Queues;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.AdminService.Services
{
    public class AdminAddedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoAdminWriteRepository _adminWriteRepository;
        private readonly ILogger<AdminAddedBGService> _logger;
        public AdminAddedBGService(
            IRabbitMQMessageService messageService,
            IMongoAdminWriteRepository adminWriteRepository,
            ILogger<AdminAddedBGService> logger)
        {
            _messageService = messageService;
            _adminWriteRepository = adminWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.ADMIN_ADDED_QUEUE,
                (message) =>
                {

                    Console.WriteLine("admin added queue çalıştı");
                });


            return Task.CompletedTask;

        }
    }
}
