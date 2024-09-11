using RentACarNow.Common.Enums.EntityEnums;
using RentACarNow.Common.Events.Common.Messages;
using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Infrastructure.Factories.Interfaces;
using System;

namespace RentACarNow.Common.Infrastructure.Factories.Implementations
{
    public class RentalEventFactory : IRentalEventFactory
    {
        public RentalCreatedEvent CreateRentalCreatedEvent(
            Guid rentalId,
            DateTime rentalStartedDate,
            DateTime rentalEndDate,
            decimal hourlyRentalPrice,
            decimal totalRentalPrice,
            Guid carId,
            string carName,
            string carModal,
            string carTitle,
            decimal carHourlyRentalPrice,
            float carKilometer,
            string carDescription,
            string carColor,
            int carPassengerCapacity,
            int carLuggageCapacity,
            float carFuelConsumption,
            DateTime? carReleaseDate,
            FuelType carFuelType,
            TransmissionType carTransmissionType,
            Guid userId,
            string userName,
            string userSurname,
            int userAge,
            string userPhoneNumber,
            string userEmail,
            RentalStatus status,
            DateTime createdDate)
        {
            var carMessage = new CarMessage
            {
                CarId = carId,
                Name = carName,
                Modal = carModal,
                Title = carTitle,
                HourlyRentalPrice = carHourlyRentalPrice,
                Kilometer = carKilometer,
                Description = carDescription,
                Color = carColor,
                PassengerCapacity = carPassengerCapacity,
                LuggageCapacity = carLuggageCapacity,
                FuelConsumption = carFuelConsumption,
                ReleaseDate = carReleaseDate,
                CarFuelType = carFuelType,
                TransmissionType = carTransmissionType
            };

            var userMessage = new UserMessage
            {
                UserId = userId,
                Name = userName,
                Surname = userSurname,
                Age = userAge,
                PhoneNumber = userPhoneNumber,
                Email = userEmail
            };

            return new RentalCreatedEvent(
                rentalId,
                rentalStartedDate,
                rentalEndDate,
                hourlyRentalPrice,
                totalRentalPrice,
                carMessage,
                userMessage,
                status,
                createdDate
            );
        }

        public RentalDeletedEvent CreateRentalDeletedEvent(Guid rentalId, DateTime deletedDate)
        {
            return new RentalDeletedEvent(rentalId, deletedDate);
        }

        public RentalUpdatedEvent CreateRentalUpdatedEvent(
            Guid rentalId,
            DateTime rentalStartedDate,
            DateTime rentalEndDate,
            decimal hourlyRentalPrice,
            decimal totalRentalPrice,
            Guid carId,
            string carName,
            string carModal,
            string carTitle,
            decimal carHourlyRentalPrice,
            float carKilometer,
            string carDescription,
            string carColor,
            int carPassengerCapacity,
            int carLuggageCapacity,
            float carFuelConsumption,
            DateTime? carReleaseDate,
            FuelType carFuelType,
            TransmissionType carTransmissionType,
            Guid userId,
            string userName,
            string userSurname,
            int userAge,
            string userPhoneNumber,
            string userEmail,
            RentalStatus status,
            DateTime updatedDate)
        {
            var carMessage = new CarMessage
            {
                CarId = carId,
                Name = carName,
                Modal = carModal,
                Title = carTitle,
                HourlyRentalPrice = carHourlyRentalPrice,
                Kilometer = carKilometer,
                Description = carDescription,
                Color = carColor,
                PassengerCapacity = carPassengerCapacity,
                LuggageCapacity = carLuggageCapacity,
                FuelConsumption = carFuelConsumption,
                ReleaseDate = carReleaseDate,
                CarFuelType = carFuelType,
                TransmissionType = carTransmissionType
            };

            var userMessage = new UserMessage
            {
                UserId = userId,
                Name = userName,
                Surname = userSurname,
                Age = userAge,
                PhoneNumber = userPhoneNumber,
                Email = userEmail
            };

            return new RentalUpdatedEvent(
                rentalId,
                rentalStartedDate,
                rentalEndDate,
                hourlyRentalPrice,
                totalRentalPrice,
                carMessage,
                userMessage,
                status,
                updatedDate
            );
        }
    }
}
