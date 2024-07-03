using RentACarNow.Common.Constants.Queues;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class QueueDeclareExtensions
    {

        public static void CreateQueues(this IRabbitMQMessageService service)
        {
            service.QueueDeclare(
                queueName: RabbitMQQueues.ADMIN_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.ADMIN_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.ADMIN_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.BRAND_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.BRAND_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.BRAND_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CAR_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CAR_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CAR_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CUSTOMER_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CUSTOMER_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CUSTOMER_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.EMPLOYEE_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.EMPLOYEE_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.EMPLOYEE_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.FEATURE_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.FEATURE_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.FEATURE_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.RENTAL_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.RENTAL_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.RENTAL_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);


            service.QueueDeclare(
                queueName: RabbitMQQueues.CLAIM_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CLAIM_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CLAIM_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);



        }


    }
}
