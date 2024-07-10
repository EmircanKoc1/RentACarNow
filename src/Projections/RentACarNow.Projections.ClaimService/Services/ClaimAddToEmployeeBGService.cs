﻿using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Claim;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.ClaimService.Services
{
    public class ClaimAddToEmployeeBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoClaimWriteRepository _claimWriteRepository;
        private readonly ILogger<ClaimAddToEmployeeBGService> _logger;

        public ClaimAddToEmployeeBGService(IRabbitMQMessageService messageService, IMongoClaimWriteRepository claimWriteRepository, ILogger<ClaimAddToEmployeeBGService> logger)
        {
            _messageService = messageService;
            _claimWriteRepository = claimWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_ADDED_TO_EMPLOYEE_QUEUE,
                message =>
                {
                    var @event = message.Deseralize<ClaimAddedToEmployeeEvent>();

                    _claimWriteRepository.AddClaimToEmployeeAsync(new Common.MongoEntities.Claim
                    {
                        CreatedDate = @event.CreatedDate,
                        DeletedDate = @event.DeletedDate,
                        Id = @event.ClaimId,
                        UpdatedDate = @event.UpdatedDate,
                        Key = @event.Key,
                        Value = @event.Value
                    }, @event.EmployeeId);
                });

            return Task.CompletedTask;
        }
    }





}