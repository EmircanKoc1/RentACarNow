﻿using MongoDB.Driver;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.MongoEntities;

namespace RentACarNow.Application.Contexts
{
    public interface IMongoDBContext
    {
        public IMongoCollection<Admin> AdminCollection { get; }
        public IMongoCollection<Customer> CustomerCollection { get; }
        public IMongoCollection<Car> CarCollection { get; }
        public IMongoCollection<Brand> BrandCollection { get; }
        public IMongoCollection<Rental> RentalCollection { get; }
        public IMongoCollection<T> GetCollection<T>() where T : IMongoEntity;




    }
}
