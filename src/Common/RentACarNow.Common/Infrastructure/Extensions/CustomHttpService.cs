using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Common.Constants.Http;
using RentACarNow.Common.Infrastructure.Services.Implementations;
using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class CustomHttpService
    {
        public static IServiceCollection AddCustomHttpService(this IServiceCollection services)
        {

            services.AddHttpClient(HttpConstants.READ_API_CLIENT_NAME, c =>
            {
                c.BaseAddress = new Uri(HttpConstants.READ_API_URI);
            });

            services.AddHttpClient(HttpConstants.WRITE_API_CLIENT_NAME, c =>
            {
                c.BaseAddress = new Uri(HttpConstants.WRITE_API_URI);
            });

            //services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IHttpService, HttpService>();

            return services;

        }

    }
}
