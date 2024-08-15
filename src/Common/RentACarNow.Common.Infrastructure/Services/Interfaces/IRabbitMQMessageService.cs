using RentACarNow.Common.Enums.SettingEnums;
using RentACarNow.Common.Events.Common;

namespace RentACarNow.Common.Infrastructure.Services.Interfaces
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

        //void PublishEvent<TEvent>(
        //    string exchangeName,
        //    TEvent @event) where TEvent : IEvent, new();

        void SendEventQueue<TEvent>(
          string exchangeName,
          string routingKey,
          TEvent @event) where TEvent : IEvent, new();

        void ConsumeQueue(
            string queueName,
            params Action<string>[] consumeOperations);

        public void ConsumeQueue(string queueName, params Func<string, Task>[] consumeOperations);
    }
}
