using RentACarNow.Common.Entities.OutboxEntities;
using RentACarNow.Common.Enums.OutboxMessageEventTypeEnums;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Events.Brand;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.OutboxPublishers.BrandOutboxPublisher
{
    internal class PublisherService : BackgroundService
    {
        private readonly IBrandOutboxRepository _outboxRepository;
        private readonly ILogger<PublisherService> _logger;
        private readonly IRabbitMQMessageService _rabbitMQMessageService;

        public PublisherService(IBrandOutboxRepository outboxRepository, ILogger<PublisherService> logger, IRabbitMQMessageService rabbitMQMessageService)
        {
            _outboxRepository = outboxRepository;
            _logger = logger;
            _rabbitMQMessageService = rabbitMQMessageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("executed");

            while (!stoppingToken.IsCancellationRequested)
            {
                IEnumerable<BrandOutboxMessage>? messages = await _outboxRepository.GetOutboxMessagesAsync(5, OrderedDirection.Descending);

                foreach (var message in messages)
                {
                    var payload = message.Payload;
                    _logger.LogInformation(payload);

                }


            }

        }
    }
}
