using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Customer;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.CustomerService.Services
{
    public class CustomerDeletedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoCustomerWriteRepository _customerWriteRepository;
        private readonly ILogger<CustomerDeletedBGService> _logger;

        public CustomerDeletedBGService(IRabbitMQMessageService messageService, IMongoCustomerWriteRepository customerWriteRepository, ILogger<CustomerDeletedBGService> logger)
        {
            _messageService = messageService;
            _customerWriteRepository = customerWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CUSTOMER_DELETED_QUEUE,
                 message =>
                {

                    var @event = message.Deseralize<CustomerDeletedEvent>();

                     _customerWriteRepository.DeleteByIdAsync(@event.Id);

                });




            return Task.CompletedTask;

        }
    }
}
