using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Domain.Events.Admin;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class AdminConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public AdminConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.ADMIN_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("admin add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.ADMIN_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("admin delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.ADMIN_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("admin update consumer çalıştı");

               });



            return Task.CompletedTask;
        }
    }
}
