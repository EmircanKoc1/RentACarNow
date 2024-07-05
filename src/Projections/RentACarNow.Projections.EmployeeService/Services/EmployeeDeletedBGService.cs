using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Events.Employee;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Projections.EmployeeService.Services
{
    public class EmployeeDeletedBGService : BackgroundService
    {

        private readonly IRabbitMQMessageService _messageService;
        private readonly IMongoEmployeeWriteRepository _employeeWriteRepository;
        private readonly ILogger<EmployeeDeletedBGService> _logger;

        public EmployeeDeletedBGService(IRabbitMQMessageService messageService, IMongoEmployeeWriteRepository employeeWriteRepository, ILogger<EmployeeDeletedBGService> logger)
        {
            _messageService = messageService;
            _employeeWriteRepository = employeeWriteRepository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.EMPLOYEE_DELETED_QUEUE,
                async message =>
                {
                    var @event = message.Deseralize<EmployeeDeletedEvent>();

                    await _employeeWriteRepository.DeleteByIdAsync(@event.Id);

                });



            return Task.CompletedTask;
        }
    }
}
