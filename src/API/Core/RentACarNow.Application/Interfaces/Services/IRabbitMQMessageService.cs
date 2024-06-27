using RentACarNow.Application.Enums;
using RentACarNow.Domain.Events;

namespace RentACarNow.Application.Interfaces.Services
{
    public interface IRabbitMQMessageService : IDisposable
    {
        void QueueDeclare(
            string queueName,
            bool durable = true,
            bool exclusive = false,
            bool autoDelete = false);
        void ExchangeDeclare(
            string exchangeName,
            bool durable = true,
            bool autoDelete = false,
            RabbitMQExchangeType exchangeType = RabbitMQExchangeType.Direct);
        void ExchangeBindQueue(
            string queueName,
            string exchangeName,
            string routingKey);

        void PublishEvent<TEvent>(
            string exchangeName,
            TEvent @event) where TEvent : IEvent, new();

        void SendEventQueue<TEvent>(
          string exchangeName,
          string routingKey,
          TEvent @event) where TEvent : IEvent, new();

        void ConsumeQueue(
            string queueName,
            params Action<string>[] consumeOperations);

    }
}
