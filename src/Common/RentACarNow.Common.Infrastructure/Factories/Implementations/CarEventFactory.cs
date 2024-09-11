using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Car;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using System;

namespace RentACarNow.Common.Infrastructure.Factories.Implementations
{
    public class CarEventFactory : ICarEventFactory
    {
        public CarCreatedEvent CreateCarCreatedEvent(
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
            DateTime createdDate)
        {
            var brandMessage = new BrandMessage
            {
                BrandId = brandId,
                Name = brandName,
                Description = brandDescription
            };

            return new CarCreatedEvent(
                carId, 
                name, 
                modal, 
                title, 
                hourlyRentalPrice, 
                kilometer, 
                description, 
                color,
                passengerCapacity, 
                luggageCapacity, 
                fuelConsumption, 
                releaseDate, 
                carFuelType,
                transmissionType, 
                brandMessage, 
                createdDate);
        }

        public CarDeletedEvent CreateCarDeletedEvent(Guid carId, DateTime deletedDate)
        {
            return new CarDeletedEvent(carId, deletedDate);
        }

        public CarFeatureAddedEvent CreateCarFeatureAddedEvent(Guid carId, Guid featureId, string name, string value)
        {
            return new CarFeatureAddedEvent(carId, featureId, name, value);
        }

        public CarFeatureDeletedEvent CreateCarFeatureDeletedEvent(Guid carId, Guid featureId)
        {
            return new CarFeatureDeletedEvent(carId, featureId);
        }

        public CarFeatureUpdatedEvent CreateCarFeatureUpdatedEvent(Guid carId, Guid featureId, string name, string value)
        {
            return new CarFeatureUpdatedEvent(carId, featureId, name, value);
        }

        public CarUpdatedEvent CreateCarUpdatedEvent(
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
            DateTime updatedDate)
        {
            var brandMessage = new BrandMessage
            {
                BrandId = brandId,
                Name = brandName,
                Description = brandDescription
            };

            return new CarUpdatedEvent(
                carId, 
                name, 
                modal, 
                title, 
                hourlyRentalPrice, 
                kilometer, 
                description, 
                color,
                passengerCapacity, 
                luggageCapacity, 
                fuelConsumption, 
                releaseDate, 
                carFuelType,
                transmissionType,
                brandMessage, 
                updatedDate);
        }
    }
}
