using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Common.Constants.MessageBrokers.Exchanges;
using RentACarNow.Common.Enums.SettingEnums;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class ExchangeDeclareExtensions
    {
        public static void CreateExchanges(this IRabbitMQMessageService service)
        //public static void CreateExchanges(this IServiceScope scope)
        //public static void CreateExchanges(this IServiceProvider provider)
        {

            //using var service = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageService>();

            //using var scope = provider.CreateScope();
            //using var service = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageService>();



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
