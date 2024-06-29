﻿using RentACarNow.Application.Constants.Exchanges;
using RentACarNow.Application.Constants.Queue;
using RentACarNow.Application.Constants.RoutingKeys;
using RentACarNow.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Infrastructure.Extensions
{
    public static class QueueBindExchangeExtensions
    {

        public static void BindExchangesAndQueues(this IRabbitMQMessageService service)
        {
            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.ADMIN_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.ADMIN_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.ADMIN_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.ADMIN_UPDATED_ROUTING_KEY);

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
                queueName: RabbitMQQueues.CUSTOMER_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CUSTOMER_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CUSTOMER_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CUSTOMER_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.CUSTOMER_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.CUSTOMER_UPDATED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.EMPLOYEE_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.EMPLOYEE_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.EMPLOYEE_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.EMPLOYEE_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.EMPLOYEE_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.EMPLOYEE_UPDATED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.FEATURE_ADDED_QUEUE,
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.FEATURE_ADDED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.FEATURE_DELETED_QUEUE,
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.FEATURE_DELETED_ROUTING_KEY);

            service.ExchangeBindQueue(
                queueName: RabbitMQQueues.FEATURE_UPDATED_QUEUE,
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                routingKey: RabbitMQRoutingKeys.FEATURE_UPDATED_ROUTING_KEY);

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



        }
    }
}
