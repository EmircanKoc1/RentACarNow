using RentACarNow.Domain.Events.Common;

namespace RentACarNow.Domain.Events.Car
{
    public class CarAddedEvent : IEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
