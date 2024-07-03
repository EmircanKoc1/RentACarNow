namespace RentACarNow.Projections.AdminService.Services
{
    using global::RentACarNow.Common.Constants.MessageBrokers.Queues;
    using global::RentACarNow.Common.Events.Admin;
    using global::RentACarNow.Common.Infrastructure.Extensions;
    using global::RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
    using global::RentACarNow.Common.Infrastructure.Services.Interfaces;

    namespace RentACarNow.Projections.AdminService.Services
    {
        public class AdminDeletedBGService : BackgroundService
        {
            private readonly IRabbitMQMessageService _messageService;
            private readonly IMongoAdminWriteRepository _adminWriteRepository;
            private readonly ILogger<AdminDeletedBGService> _logger;
            public AdminDeletedBGService(
                IRabbitMQMessageService messageService,
                IMongoAdminWriteRepository adminWriteRepository,
                ILogger<AdminDeletedBGService> logger)
            {
                _messageService = messageService;
                _adminWriteRepository = adminWriteRepository;
                _logger = logger;
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {

                _messageService.ConsumeQueue(
                    queueName: RabbitMQQueues.ADMIN_DELETED_QUEUE,
                    (message) =>
                    {
                        var @event = message.Deseralize<AdminDeletedEvent>();

                        _adminWriteRepository.DeleteByIdAsync(@event.Id);

                        _logger.LogInformation("Message received");
                        Console.WriteLine("admin deleted queue çalıştı");
                    });


                return Task.CompletedTask;

            }
        }
    }

}
