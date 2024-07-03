﻿using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Admin;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

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
                    var @event = message.Deseralize<AdminAddedEvent>();

                    _adminWriteRepository.AddAsync(new Admin
                    {
                        Email = @event.Email,
                        Password = @event.Password,
                        Username = @event.Username,
                        Claims = null,
                        CreatedDate = DateTime.UtcNow,
                        DeletedDate = null,
                        UpdatedDate = null

                    });

                    _logger.LogInformation("Message received");
                    Console.WriteLine("admin added queue çalıştı");
                });


            return Task.CompletedTask;

        }
    }
}
