using RentACarNow.Common.Contexts.InboxContexts.Implementations;
using RentACarNow.Common.Entities.InboxEntities;
using RentACarNow.Common.Infrastructure.Repositories.Implementations.Base;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Unified;

namespace RentACarNow.Common.Infrastructure.Repositories.Implementations.Unified
{
    public class UserInboxMongoRepository : BaseInboxMongoRepository<UserInboxMessage, MongoUserInboxContext>, IUserInboxRepository
    {
        public UserInboxMongoRepository(MongoUserInboxContext context) : base(context)
        {
        }
    }


}
