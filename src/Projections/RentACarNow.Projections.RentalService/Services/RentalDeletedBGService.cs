using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.RentalService.Services
{
    public class RentalDeletedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoRentalWriteRepository _rentalWriteRepository;
        private readonly ILogger<RentalDeletedBGService> _logger;

        public RentalDeletedBGService(IRabbitMQMessageService messageService, IMongoRentalWriteRepository rentalWriteRepository, ILogger<RentalDeletedBGService> logger)
        {
            _messageService = messageService;
            _rentalWriteRepository = rentalWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.RENTAL_DELETED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<RentalDeletedEvent>();

                    await _rentalWriteRepository.DeleteByIdAsync(@event.Id);


                });

            return Task.CompletedTask;

        }
    }
}
