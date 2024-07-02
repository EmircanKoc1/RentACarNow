using Microsoft.Extensions.Hosting;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Common.Constants.Queues;
using RentACarNow.Common.Infrastructure.Services;
using RentACarNow.Domain.Events.Rental;
using RentACarNow.Infrastructure.Extensions;

namespace RentACarNow.Infrastructure.BackgroundServices
{
    public class RentalConsumerBG : BackgroundService
    {
        private readonly IRabbitMQMessageService _messageService;

        public RentalConsumerBG(IRabbitMQMessageService messageService)
        {
            _messageService = messageService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.RENTAL_ADDED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("rental add consumer çalıştı");

                });

            _messageService.ConsumeQueue(
                queueName: RabbitMQQueues.RENTAL_DELETED_QUEUE,
                (@event) =>
                {
                    Console.WriteLine("rental delete consumer çalıştı");

                });

            _messageService.ConsumeQueue(
               queueName: RabbitMQQueues.RENTAL_UPDATED_QUEUE,
               (@event) =>
               {
                   Console.WriteLine("rental update consumer çalıştı");

               });

            return Task.CompletedTask;
        }
    }
}
