using RentACarNow.Common.Events.Rental;
using RentACarNow.Common.Enums.EntityEnums;
using System;

namespace RentACarNow.Common.Infrastructure.Factories.Interfaces
{
    public interface IRentalEventFactory
    {
        RentalCreatedEvent CreateRentalCreatedEvent(
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
            DateTime createdDate);

        RentalUpdatedEvent CreateRentalUpdatedEvent(
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
            DateTime updatedDate);

        RentalDeletedEvent CreateRentalDeletedEvent(
            Guid rentalId,
            DateTime deletedDate);
    }
}
