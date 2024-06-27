using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Application.Enums;
using RentACarNow.Application.Interfaces.Services;
using System.Reflection;

namespace RentACarNow.Persistence.Services
{
    public class IoCRegisterService : IIoCRegisterService
    {
        public IServiceCollection RegisterServices(IServiceCollection services,
            Type findType,
            RegisterLifeTime registerLifeTime = RegisterLifeTime.Scoped)
        {
            var asm = Assembly.GetExecutingAssembly();

            var interfaceServices = asm.GetTypes()
                 .Where(type => type.IsInterface == true)
                 .Where(type => type.GetInterfaces().Contains(findType))
                 .ToList();

            interfaceServices.ForEach(interfaceService =>
            {

                var concreteServices = asm.GetTypes()
               .Where(type => type.IsClass == true)
               .Where(type => type.GetInterfaces().Contains(interfaceService))
               .ToList();


                concreteServices.ForEach(concreteService =>
                {
                    services.AddScoped(interfaceService, concreteService);

                });

            });

            return services;
        }


    }
}
