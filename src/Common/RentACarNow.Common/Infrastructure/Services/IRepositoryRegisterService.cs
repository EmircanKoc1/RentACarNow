using Microsoft.Extensions.DependencyInjection;
using RentACarNow.Common.Enums.SettingEnums;

namespace RentACarNow.Common.Infrastructure.Services
{
    public interface IIoCRegisterService
    {
        IServiceCollection RegisterServices(IServiceCollection services,
            Type findType,
            RegisterLifeTime registerLifeTime = RegisterLifeTime.Scoped);



    }
}
