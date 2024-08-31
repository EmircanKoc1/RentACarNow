namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureUpdatedEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid CarId { get; set; }

    }
}
