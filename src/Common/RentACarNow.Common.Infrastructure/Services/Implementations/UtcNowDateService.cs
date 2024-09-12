using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Services.Implementations
{
    public class UtcNowDateService : IDateService
    {
        public DateTime GetDate() => DateTime.UtcNow;
    }
}
