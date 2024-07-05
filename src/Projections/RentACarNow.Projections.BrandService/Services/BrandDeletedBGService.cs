﻿using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Projections.BrandService.Services
{
    public class BrandDeletedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly MongoBrandWriteRepository _brandWriteRepository;
        private readonly ILogger<BrandDeletedBGService> _logger;

        public BrandDeletedBGService(IRabbitMQMessageService messageService,
            MongoBrandWriteRepository brandWriteRepository,
            ILogger<BrandDeletedBGService> logger)
        {
            _messageService = messageService;
            _brandWriteRepository = brandWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName : RabbitMQQueues.BRAND_DELETED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<BrandDeletedEvent>();

                   await _brandWriteRepository.DeleteByIdAsync(@event.Id);


                });


            return Task.CompletedTask;
        }
    }
}
