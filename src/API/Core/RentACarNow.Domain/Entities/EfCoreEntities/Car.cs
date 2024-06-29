﻿using RentACarNow.Domain.Entities.Common.Concrete;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Enums;

namespace RentACarNow.Domain.Entities.EfCoreEntities
{
    public class Car : BaseEntity, IEfEntity
    {

        public string Name { get; set; }
        public string Modal { get; set; }
        public string Title { get; set; }
        public decimal HourlyRentalPrice { get; set; }
        public float Kilometer { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int PassengerCapacity { get; set; }
        public int LuggageCapacity { get; set; }
        public float FuelConsumption { get; set; }
        public FuelType CarFuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Brand brand { get; set; }
        ICollection<Feature> Features { get; set; }
        ICollection<Rental> Rentals { get; set; }

    }
}