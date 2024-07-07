﻿using RentACarNow.Common.Constants.MessageBrokers.Queues;
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
        public ClaimAddToCustomerBGService(IRabbitMQMessageService messageService, IMongoClaimWriteRepository claimWriteRepository, ILogger<ClaimAddToCustomerBGService> logger)
        {
            _messageService = messageService;
            _claimWriteRepository = claimWriteRepository;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_ADDED_TO_CUSTOMER_QUEUE,
                message =>
                {
                    var @event = message.Deseralize<ClaimAddedToCustomerEvent>();

                    _claimWriteRepository.AddClaimToCustomerAsync(new Common.MongoEntities.Claim
                    {

                        CreatedDate = @event.CreatedDate,
                        DeletedDate = @event.DeletedDate,
                        UpdatedDate = @event.UpdatedDate,
                        Id = @event.ClaimId,
                        Key = @event.Key,
                        Value = @event.Value

                    },@event.CustomerId);

                });


            return Task.CompletedTask;
        }
    }





}
