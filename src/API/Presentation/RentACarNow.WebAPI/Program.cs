using RentACarNow.Application.Features.CQRS.Queries.Admin.GetAll;
using RentACarNow.Infrastructure;
using RentACarNow.Persistence;
using System.Reflection;
using RentACarNow.Application;
using RentACarNow.Application.Interfaces.Services;
using RentACarNow.Infrastructure.Extensions;
using RentACarNow.Infrastructure.BackgroundServices;
using RentACarNow.Common.Infrastructure.Services;
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
            builder.Services.AddApplicationServices();


            builder.Services.AddHostedService<AdminConsumerBG>();

            var app = builder.Build();


            #region rabbitmq

            using var scope = app.Services.CreateScope();
            var myservice = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageService>();

            myservice.CreateQueues();
            myservice.CreateExchanges();
            myservice.BindExchangesAndQueues();


            #endregion

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
