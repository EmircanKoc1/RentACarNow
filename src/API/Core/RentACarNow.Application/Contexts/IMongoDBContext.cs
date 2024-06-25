using MongoDB.Driver;
using RentACarNow.Domain.Entities.Common.Interfaces;
using RentACarNow.Domain.Entities.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Contexts
{
    public interface IMongoDBContext
    {
       public IMongoCollection<Admin> AdminCollection { get;  }
       public IMongoCollection<Customer> CustomerCollection { get;  }
       public IMongoCollection<Car> CarCollection { get;  }
       public IMongoCollection<Brand> BrandCollection { get; }
       //public IMongoCollection<T> GetCollection<T>() where T : IMongoEntity;
           



    }
}
