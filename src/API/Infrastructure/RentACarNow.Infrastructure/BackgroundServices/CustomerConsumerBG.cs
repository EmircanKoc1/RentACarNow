using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class CustomerConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public CustomerConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CUSTOMER_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("customer add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CUSTOMER_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("customer delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.CUSTOMER_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("customer update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
