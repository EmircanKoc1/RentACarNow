
using RentACarNow.Persistence;
using RentACarNow.Infrastructure;
using RentACarNow.Infrastructure.Services;
using RentACarNow.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

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
            
            using var scope = app.Services.CreateScope();
            var myservice = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageService>();

         

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
