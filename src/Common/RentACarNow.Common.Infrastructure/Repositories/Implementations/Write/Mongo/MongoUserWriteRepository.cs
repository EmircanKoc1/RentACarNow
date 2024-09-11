using MongoDB.Driver;
using RentACarNow.Common.Entities.MongoEntities;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Write.Mongo;
using RentACarNow.Common.MongoContexts.Implementations;
using RentACarNow.Common.MongoEntities;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Write.Mongo
{
    public class MongoUserWriteRepository : MongoBaseWriteRepository<User>, IMongoUserWriteRepository
    {
        public MongoUserWriteRepository(MongoRentalACarNowDbContext context) : base(context)
        {
        }

        public async Task AddUserClaimAsync(Guid userId, Claim entity)
        {
            var filterDefinition = Builders<User>.Filter
                .Eq(u => u.Id, userId);


            var updateDefinition = Builders<User>.Update
                .Push(u => u.Claims, entity);

            await _collection.UpdateOneAsync(filterDefinition, updateDefinition);
        }

        public async Task DeleteUserClaimAsync(Guid userId, Guid claimId)
        {
            var filterDefinition = Builders<User>.Filter
                .Eq(u => u.Id, userId);

            var claimFilterDefinition = Builders<Claim>.Filter
                .Eq(c => c.Id, claimId);

            var updateDefinition = Builders<User>.Update
                .PullFilter(u => u.Claims, claimFilterDefinition);


            await _collection.UpdateOneAsync(filterDefinition, updateDefinition);

        }

        public override async Task UpdateAsync(User entity)
        {
            var filterDefinition = Builders<User>.Filter
                .Eq(u => u.Id, entity.Id);

            var updateDefinition = Builders<User>.Update
                .Set(u => u.Age, entity.Age)
                .Set(u => u.Name, entity.Name)
                .Set(u => u.Surname, entity.Surname)
                .Set(u => u.PhoneNumber, entity.PhoneNumber)
                .Set(u => u.Password, entity.Password)
                .Set(u => u.UpdatedDate, entity.UpdatedDate);

            await _collection.UpdateOneAsync(filterDefinition, updateDefinition);


        }

        public async Task UpdateUserClaimAsync(Claim claim)
        {

            var filterDefinition = Builders<User>.Filter
                .ElemMatch(u => u.Claims, Builders<Claim>.Filter.Eq(c => c.Id, claim.Id));

            var updateDefinition = Builders<User>.Update
                .Set("Claims.$[elem].Key", claim.Key)
                .Set("Claims.$[elem].Value", claim.Value)
                .Set("Claims.$[elem].UpdatedDate", claim.UpdatedDate);

            await _collection.UpdateManyAsync(filterDefinition, updateDefinition);


        }
    }


}
