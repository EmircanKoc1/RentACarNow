using RentACarNow.Common.Infrastructure.Services.Interfaces;

namespace RentACarNow.Common.Infrastructure.Services.Implementations
{
    public class GuidService : IGuidService
    {
        public Guid CreateGuid() => Guid.NewGuid();

        public Guid GetEmptyGuid() => Guid.Empty;

    }
}
