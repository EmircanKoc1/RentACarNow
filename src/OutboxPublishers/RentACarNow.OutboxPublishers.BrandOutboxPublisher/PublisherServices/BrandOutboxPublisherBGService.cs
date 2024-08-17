using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;

namespace RentACarNow.OutboxPublishers.BrandOutboxPublisher.PublisherServices
{
    internal class BrandOutboxPublisherBGService : BackgroundService
    {
        private readonly IBrandOutboxRepository _brandOutboxRepository;
        private readonly ILogger<BrandOutboxPublisherBGService> _logger;
        public BrandOutboxPublisherBGService(IBrandOutboxRepository brandOutboxRepository, ILogger<BrandOutboxPublisherBGService> logger)
        {
            _brandOutboxRepository = brandOutboxRepository;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("execute edildi");

            await _brandOutboxRepository.AddMessageAsync(new Common.Entities.OutboxEntities.BrandOutboxMessage
            {
                Id = Guid.NewGuid(),
                Payload = "emircsssssan",
                AddedDate = DateTime.UtcNow,
                IsPublished = false,
                PublishDate = null,
            });
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("döngğye girdi");
                var result = await _brandOutboxRepository.GetOutboxMessagesAsync(5, OrderedDirection.Ascending);

                foreach (var message in result)
                    Console.WriteLine(message.Payload);

                await Task.Delay(TimeSpan.FromSeconds(2));
            }

        }
    }
}
