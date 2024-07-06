using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Customer;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.CustomerService.Services
{
    public class CustomerAddedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoCustomerWriteRepository _customerWriteRepository;
        private readonly ILogger<CustomerAddedBGService> _logger;

        public CustomerAddedBGService(IRabbitMQMessageService messageService,
            IMongoCustomerWriteRepository customerWriteRepository,
            ILogger<CustomerAddedBGService> logger)
        {
            _messageService = messageService;
            _customerWriteRepository = customerWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CUSTOMER_ADDED_QUEUE,
                 message =>
                {
                    var @event = message.Deseralize<CustomerAddedEvent>();

                     _customerWriteRepository.AddAsync(new Customer
                    {
                        Id = @event.Id,
                        Age = @event.Age,
                        Name = @event.Name,
                        Email = @event.Email,
                        Password = @event.Password,
                        PhoneNumber = @event.PhoneNumber,
                        Surname = @event.Surname,
                        Username = @event.Username,
                        WalletBalance = @event.WalletBalance,
                        CreatedDate = @event.CreatedDate,
                        DeletedDate = @event.DeletedDate,
                        UpdatedDate = @event.UpdatedDate,
                        Claims = @event.Claims.Select(x => new Claim
                        {
                            Id = x.Id,
                            Key = x.Key,
                            Value = x.Value ,
                            CreatedDate= x.CreatedDate,
                            DeletedDate= x.DeletedDate,
                            UpdatedDate = x.UpdatedDate
                            
                        }).ToList()
                    });



                });


            return Task.CompletedTask;

        }
    }
}
