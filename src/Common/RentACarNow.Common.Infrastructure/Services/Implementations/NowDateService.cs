using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Services.Implementations
{

    public class NowDateService : IDateService
    {
        public DateTime GetDate() => DateTime.Now;
    }
}
