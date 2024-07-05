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
                async (message) =>
                {
                    var @event = message.Deseralize<AdminUpdatedEvent>();

                    if (@event.Claims is null || !@event.Claims.Any())
                    {
                        await _adminWriteRepository.UpdateAsync(
                              new Admin
                              {
                                  UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
                                  Password = @event.Password,
                                  Username = @event.Username,
                                  Email = @event.Email,
                                  CreatedDate = @event.CreatedDate,
                                  DeletedDate = @event.DeletedDate,

                              });

                    }
                    else
                    {

                        await _adminWriteRepository.UpdateWithRelationDatasAsync(
                            new Admin
                            {
                                Username = @event.Username,
                                Email = @event.Email,
                                Password = @event.Password,
                                CreatedDate = @event.CreatedDate,
                                DeletedDate = @event.DeletedDate,
                                UpdatedDate = @event.UpdatedDate,
                                Claims = @event.Claims.Select(cm => new Claim
                                {
                                    Id = cm.Id,
                                    Key = cm.Key,
                                    Value = cm.Value,
                                    CreatedDate = cm.CreatedDate,
                                    UpdatedDate = cm.UpdatedDete ?? DateTime.Now,
                                    DeletedDate = cm.DeletedDate
                                }).ToList()
                            });


                    }

                    _logger.LogInformation("Message received");
                    Console.WriteLine("admin updated queue çalıştı");
                });


            return Task.CompletedTask;

        }
    }
}
