using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Car;

namespace RentACarNow.Common.Infrastructure.Factories.Interfaces
{
    public interface ICarEventFactory
    {
        CarCreatedEvent CreateCarCreatedEvent(
            Guid carId,
            string name,
            string modal,
            string title,
            decimal hourlyRentalPrice,
            float kilometer,
            string description,
            string color,
            int passengerCapacity,
            int luggageCapacity,
            float fuelConsumption,
            DateTime? releaseDate,
            FuelType carFuelType,
            TransmissionType transmissionType,
            Guid brandId,
            string brandName,
            string brandDescription,
            DateTime createdDate);

        CarDeletedEvent CreateCarDeletedEvent(
            Guid carId,
            DateTime deletedDate);

        CarUpdatedEvent CreateCarUpdatedEvent(
            Guid carId,
            string name,
            string modal,
            string title,
            decimal hourlyRentalPrice,
            float kilometer,
            string description,
            string color,
            int passengerCapacity,
            int luggageCapacity,
            float fuelConsumption,
            DateTime? releaseDate,
            FuelType carFuelType,
            TransmissionType transmissionType,
            Guid brandId,
            string brandName,
            string brandDescription,
            DateTime updatedDate);


        CarFeatureAddedEvent CreateCarFeatureAddedEvent(
            Guid carId,
            Guid featureId,
            string name,
            string value);

        CarFeatureUpdatedEvent CreateCarFeatureUpdatedEvent(
           Guid carId,
           Guid featureId,
           string name,
           string value);

        CarFeatureDeletedEvent CreateCarFeatureDeletedEvent(
            Guid carId,
            Guid featureId);

    }
}
