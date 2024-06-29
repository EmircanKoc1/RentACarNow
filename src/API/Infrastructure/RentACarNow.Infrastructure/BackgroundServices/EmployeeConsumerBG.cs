using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Domain.Events.Employee;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class EmployeeConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public EmployeeConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.EMPLOYEE_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("employee add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.EMPLOYEE_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("employee delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.EMPLOYEE_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("employee update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
