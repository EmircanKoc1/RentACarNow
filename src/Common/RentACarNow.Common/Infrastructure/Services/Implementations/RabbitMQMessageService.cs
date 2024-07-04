using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentACarNow.Common.Enums.SettingEnums;
using RentACarNow.Common.Events.Common;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Services.Implementations
{
    public class RabbitMQMessageService : IRabbitMQMessageService
    {
        protected readonly ConnectionFactory _connectionFactory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;


        public RabbitMQMessageService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }


        public void QueueDeclare(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false)
        {

            if (string.IsNullOrWhiteSpace(queueName))
                throw new Exception("The Queue name must not be null or empty");



            _channel.QueueDeclare(
              queue: queueName,
              durable: durable,
              exclusive: exclusive,
              autoDelete: autoDelete);
        }


        public void ExchangeDeclare(string exchangeName, bool durable = true, bool autoDelete = false, RabbitMQExchangeType exchangeType = RabbitMQExchangeType.Direct)
        {
            if (string.IsNullOrWhiteSpace(exchangeName))
                throw new Exception("The Queue name must not be null or empty");




            var exchange = exchangeType switch
            {
                RabbitMQExchangeType.Fanout => ExchangeType.Fanout,
                RabbitMQExchangeType.Direct => ExchangeType.Direct,
                RabbitMQExchangeType.Topic => ExchangeType.Topic,
                RabbitMQExchangeType.Header => ExchangeType.Headers,
            };

            _channel.ExchangeDeclare(
                exchange: exchangeName,
                type: exchange,
                durable: durable,
                autoDelete: autoDelete);

        }


        public void ExchangeBindQueue(string queueName, string exchangeName, string routingKey)
        {
            if (string.IsNullOrWhiteSpace(queueName) || string.IsNullOrEmpty(exchangeName))
                throw new Exception("Queue name or Exchange name must not be null or empty");


            _channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: routingKey);


        }


        public void SendEventQueue<TEvent>(string exchangeName, string routingKey, TEvent @event) where TEvent : IEvent, new()
        {

            var stringEvent = @event.Serialize();

            ReadOnlyMemory<byte> byteMessage = stringEvent.ConvertToByteArray();

            _channel.BasicPublish(
                exchange: exchangeName,
                routingKey: routingKey,
                body: byteMessage);

        }

        public void ConsumeQueue(string queueName, params Action<string>[] consumeOperations)
        {
            var consumer = new EventingBasicConsumer(_channel);

            foreach (var consumeOperation in consumeOperations)
            {
                consumer.Received += (sender, e) =>
                {
                    consumeOperation(e.Body.Span.ConvertToString());

                };

            }

            _channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);


        }


        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }


    }
}
