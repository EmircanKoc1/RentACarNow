using RentACarNow.Common.Constants.MessageBrokers.Queues;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class QueueDeclareExtensions
    {

        public static void CreateQueues(this IRabbitMQMessageService service)
        {


            #region Brand

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

            #endregion

            #region Car
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
                queueName: RabbitMQQueues.CAR_FEATURE_ADDED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CAR_FEATURE_DELETED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);

            service.QueueDeclare(
                queueName: RabbitMQQueues.CAR_FEATURE_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);
            #endregion

            #region Rental 
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

            #endregion

            #region  Claim

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

            #endregion

            service.QueueDeclare(
                queueName: RabbitMQQueues.USER_CLAIM_UPDATED_QUEUE,
                durable: true,
                exclusive: false,
                autoDelete: false);




        }


    }
}
