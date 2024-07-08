using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll
{
    public class GetAllClaimQueryRequestHandler : IRequestHandler<GetAllClaimQueryRequest, IEnumerable<GetAllClaimQueryResponse>>
    {
        private readonly IMongoClaimReadRepository _readRepository;
        private readonly ILogger<GetAllClaimQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllClaimQueryRequestHandler(IMongoClaimReadRepository repository, ILogger<GetAllClaimQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllClaimQueryResponse>> Handle(GetAllClaimQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllClaimQueryResponse>>(result);
        }
    }
}
