using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Domain.Events.Brand;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class BrandConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public BrandConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("brand add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.BRAND_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("brand delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.BRAND_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("brand update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
