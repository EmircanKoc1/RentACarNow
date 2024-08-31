using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Constants.MessageBrokers.RoutingKeys;
using RentACarNow.Common.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class QueueBindExchangeExtensions
    {

        public static void BindExchangesAndQueues(this IRabbitMQMessageService service)
        {
            

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.BRAND_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.BRAND_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.BRAND_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.BRAND_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.BRAND_UPDATED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CAR_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CAR_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CAR_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CAR_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CAR_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CAR_UPDATED_ROUTING_KEY);

            service.ExchangeBindQueue(
              queueName: RabbitMQQueues.CAR_FEATURE_UPDATED_QUEUE,
              exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
              routingKey: RabbitMQRoutingKeys.CAR_FEATURE_UPDATED_ROUTING_KEY);

            service.ExchangeBindQueue(
              queueName: RabbitMQQueues.CAR_FEATURE_DELETED_QUEUE,
              exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
              routingKey: RabbitMQRoutingKeys.CAR_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
              queueName: RabbitMQQueues.CAR_FEATURE_ADDED_QUEUE,
              exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
              routingKey: RabbitMQRoutingKeys.CAR_FEATURE_ADDED_ROUTING_KEY);




            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.RENTAL_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.RENTAL_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.RENTAL_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.RENTAL_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.RENTAL_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.RENTAL_UPDATED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CLAIM_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CLAIM_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CLAIM_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CLAIM_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CLAIM_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CLAIM_UPDATED_ROUTING_KEY);

          

          

        }
    }
}
