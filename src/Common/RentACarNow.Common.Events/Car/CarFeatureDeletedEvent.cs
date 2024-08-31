namespace RentACarNow.Common.Events.Car
{
    public class CarFeatureDeletedEvent
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
    }
}
