using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Common.Constants.Queues;
using RentACarNow.Common.Infrastructure.Services;
using RentACarNow.Domain.Events.Feature;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class FeatureConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public FeatureConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.FEATURE_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("feature add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.FEATURE_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("feature delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.FEATURE_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("feature update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
