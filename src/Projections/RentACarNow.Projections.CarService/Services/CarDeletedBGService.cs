using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.CarService.Services
{
    public class CarDeletedBGService : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoCarWriteRepository _carWriteRepository;
        private readonly ILogger<CarDeletedBGService> _logger;

        public CarDeletedBGService(IRabbitMQMessageService messageService, IMongoCarWriteRepository carWriteRepository, ILogger<CarDeletedBGService> logger)
        {
            _messageService = messageService;
            _carWriteRepository = carWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CAR_DELETED_QUEUE,
                async message =>
                {

                    var @event = message.Deseralize<CarDeletedEvent>();
                    await _carWriteRepository.DeleteByIdAsync(@event.Id);

                });



            return Task.CompletedTask;
        }
    }
}
