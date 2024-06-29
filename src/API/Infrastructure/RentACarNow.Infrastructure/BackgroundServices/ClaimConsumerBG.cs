using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class ClaimConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public ClaimConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.,
                (@event) =>
                {
                    Console.WriteLine("claim add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CLAIM_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("claim delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.CLAIM_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("claim update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
