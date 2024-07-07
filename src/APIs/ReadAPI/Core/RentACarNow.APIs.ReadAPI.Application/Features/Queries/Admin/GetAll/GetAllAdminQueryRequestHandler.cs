using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequestHandler : IRequestHandler<GetAllAdminQueryRequest, IEnumerable<GetAllAdminQueryResponse>>
    {
        private readonly IMongoAdminReadRepository _readRepository;
        private readonly ILogger<GetAllAdminQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllAdminQueryRequestHandler(IMongoAdminReadRepository repository, ILogger<GetAllAdminQueryRequestHandler> logger)
        {
            _readRepository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllAdminQueryResponse>> Handle(GetAllAdminQueryRequest request, CancellationToken cancellationToken)
        {

            var pagParam = new PaginationParameters(20, 1);
            var result = await _readRepository.GetAllAsync(pagParam);



            return result.Select(x => new GetAllAdminQueryResponse
            {
                Id = x.Id
            });

        }
    }

}
