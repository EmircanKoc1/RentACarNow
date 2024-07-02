using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Application.Enums;

namespace RentACarNow.Application.Interfaces.Services
{
    public interface IIoCRegisterService
    {
        IServiceCollection RegisterServices(IServiceCollection services,
            Type findType ,
            RegisterLifeTime registerLifeTime = RegisterLifeTime.Scoped);



    }
}
