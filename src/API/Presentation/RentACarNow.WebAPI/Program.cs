
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Domain.Events.Common;
using RentACarNow.Infrastructure;
using RentACarNow.Infrastructure.Extensions;
using RentACarNow.Persistence;

namespace RentACarNow.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigPersistenceServices(builder.Configuration);
            builder.Services.ConfigInfrastructureLayer();


            var app = builder.Build();

            //using var scope = app.Services.CreateScope();
            //var myservice = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageService>();

            //myservice.QueueDeclare("deneme kuyruk");
            //myservice.QueueDeclare("deneme kuyruk2");

            //myservice.ExchangeDeclare("deneme exchange");
            //myservice.ExchangeDeclare("deneme fanoutex",exchangeType:Application.Enums.RabbitMQExchangeType.Fanout);
            
            //myservice.ExchangeBindQueue("deneme kuyruk", "deneme fanoutex", string.Empty);
            //myservice.ExchangeBindQueue("deneme kuyruk2", "deneme fanoutex", string.Empty);


            //myservice.ConsumeQueue("deneme kuyruk", (@event) =>
            //{
            //    Console.WriteLine(@event.Deseralize<CarAddedEvent>().Id);
            //    Console.WriteLine(@event.Deseralize<CarAddedEvent>().Name);

            //});

            //myservice.ConsumeQueue("deneme kuyruk2", (@event) =>
            //{
            //    Console.WriteLine(@event.Deseralize<CarAddedEvent>().Id);
            //    Console.WriteLine(@event.Deseralize<CarAddedEvent>().Name);

            //});

            //Enumerable.Range(1, 2).ToList().ForEach(x =>
            //{
            //    myservice.SendEventQueue<CarAddedEvent>("deneme fanoutex", string.Empty, new() { Id = Guid.NewGuid(), Name = "tofaþ" + x });

            //});

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
