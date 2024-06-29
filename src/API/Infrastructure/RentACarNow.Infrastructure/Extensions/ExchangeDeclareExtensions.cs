using RentACarNow.Application.Constants.Exchanges;
using RentACarNow.Application.Enums;
using RentACarNow.Application.Interfaces.Services;

namespace RentACarNow.Infrastructure.Extensions
{
    public static class ExchangeDeclareExtensions
    {
        public static void CreateExchanges(this IRabbitMQMessageService service)
        {

            service.ExchangeDeclare(
                exchangeName: RabbitMQExchanges.ADMIN_EXCHANGE,
                durable: true,
                autoDelete: false,
                RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
                exchangeName: RabbitMQExchanges.BRAND_EXCHANGE,
                durable: true,
                autoDelete: false,
                RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
                exchangeName: RabbitMQExchanges.CAR_EXCHANGE,
                durable: true,
                autoDelete: false,
                RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
                exchangeName: RabbitMQExchanges.CUSTOMER_EXCHANGE,
                durable: true,
                autoDelete: false,
                RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
                exchangeName: RabbitMQExchanges.EMPLOYEE_EXCHANGE,
                durable: true,
                autoDelete: false,
                RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
                exchangeName: RabbitMQExchanges.FEATURE_EXCHANGE,
                durable: true,
                autoDelete: false,
                RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
               exchangeName: RabbitMQExchanges.RENTAL_EXCHANGE,
               durable: true,
               autoDelete: false,
               RabbitMQExchangeType.Direct);

            service.ExchangeDeclare(
               exchangeName: RabbitMQExchanges.CLAIM_EXCHANGE,
               durable: true,
               autoDelete: false,
               RabbitMQExchangeType.Direct);




        }


    }
}
