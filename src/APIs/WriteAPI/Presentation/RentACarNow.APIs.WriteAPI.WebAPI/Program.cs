using RentACarNow.APIs.WriteAPI.Application;
using RentACarNow.APIs.WriteAPI.Persistence;
using RentACarNow.APIs.WriteAPI.WebAPI.Extensions;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.APIs.WriteAPI.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

       

            builder.Services.SetAuthorize(false);

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);

            var app = builder.Build();

            app.Services.GetService<IRabbitMQMessageService>()?.CreateExchanges();
            app.Services.GetService<IRabbitMQMessageService>()?.CreateQueues();
            app.Services.GetService<IRabbitMQMessageService>()?.BindExchangesAndQueues();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
