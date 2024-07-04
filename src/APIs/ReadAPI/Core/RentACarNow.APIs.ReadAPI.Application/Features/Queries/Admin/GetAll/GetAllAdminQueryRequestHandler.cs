using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequestHandler : IRequestHandler<GetAllAdminQueryRequest, IEnumerable<GetAllAdminQueryResponse>>
    {
        private readonly IMongoAdminReadRepository _repository;
        private readonly ILogger<GetAllAdminQueryRequestHandler> _logger;

        public GetAllAdminQueryRequestHandler(IMongoAdminReadRepository repository, ILogger<GetAllAdminQueryRequestHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllAdminQueryResponse>> Handle(GetAllAdminQueryRequest request, CancellationToken cancellationToken)
        {



            return null;
            
        }
    }

}
