using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.AdminService.Services
{

    public class AdminUpdatedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoAdminWriteRepository _adminWriteRepository;
        private readonly ILogger<AdminUpdatedBGService> _logger;
        public AdminUpdatedBGService(
            IRabbitMQMessageService messageService,
            IMongoAdminWriteRepository adminWriteRepository,
            ILogger<AdminUpdatedBGService> logger)
        {
            _messageService = messageService;
            _adminWriteRepository = adminWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.ADMIN_UPDATED_QUEUE,
                (message) =>
                {
                    var @event = message.Deseralize<AdminUpdatedEvent>();

                    _adminWriteRepository.UpdateAsync(new Admin
                    {
                        Email = @event.Email,
                        


                    });

                    _logger.LogInformation("Message received");
                    Console.WriteLine("admin updated queue çalıştı");
                });


            return Task.CompletedTask;

        }
    }
}
