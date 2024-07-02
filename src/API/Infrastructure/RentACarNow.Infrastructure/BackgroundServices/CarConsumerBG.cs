using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Common.Constants.Queues;
using RentACarNow.Common.Infrastructure.Services;
using RentACarNow.Domain.Events.Car;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class CarConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public CarConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CAR_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("car add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.CAR_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("car delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.CAR_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("car update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
