using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Customer;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Projections.CustomerService.Services
{
    public class CustomerUpdatedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoCustomerWriteRepository _customerWriteRepository;
        private readonly ILogger<CustomerUpdatedBGService> _logger;

        public CustomerUpdatedBGService(IRabbitMQMessageService messageService, IMongoCustomerWriteRepository customerWriteRepository, ILogger<CustomerUpdatedBGService> logger)
        {
            _messageService = messageService;
            _customerWriteRepository = customerWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CUSTOMER_UPDATED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<CustomerUpdatedEvent>();

                    if (@event.Claims is null || !@event.Claims.Any())
                    {

                        await _customerWriteRepository.UpdateWithRelationDatasAsync(new Customer
                        {
                            Id = @event.Id,
                            Age = @event.Age,
                            Email = @event.Email,
                            Name = @event.Name,
                            Password = @event.Password,
                            PhoneNumber = @event.PhoneNumber,
                            Surname = @event.Surname,
                            Username = @event.Username,
                            CreatedDate = @event.CreatedDate,
                            UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
                            DeletedDate = @event.DeletedDate,
                            WalletBalance = @event.WalletBalance
                        });

                    }

                    else
                    {
                        await _customerWriteRepository.UpdateWithRelationDatasAsync(new Customer
                        {
                            Id = @event.Id,
                            Age = @event.Age,
                            Email = @event.Email,
                            Name = @event.Name,
                            Password = @event.Password,
                            PhoneNumber = @event.PhoneNumber,
                            Surname = @event.Surname,
                            Username = @event.Username,
                            CreatedDate = @event.CreatedDate,
                            UpdatedDate = @event.UpdatedDate ?? DateTime.Now,
                            DeletedDate = @event.DeletedDate,
                            WalletBalance = @event.WalletBalance,
                            Claims = @event.Claims.Select(cm => new Claim
                            {
                                Id = cm.Id,
                                Key = cm.Key,
                                Value = cm.Value,
                                CreatedDate = cm.CreatedDate,
                                DeletedDate = cm.DeletedDate,
                                UpdatedDate = cm.UpdatedDate ?? DateTime.Now

                            }).ToList()
                        });

                    }



                });





            return Task.CompletedTask;

        }
    }
}
