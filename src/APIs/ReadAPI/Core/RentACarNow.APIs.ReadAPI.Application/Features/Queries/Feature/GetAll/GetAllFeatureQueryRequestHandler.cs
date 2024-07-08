using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll
{
    public class GetAllFeatureQueryRequestHandler : IRequestHandler<GetAllFeatureQueryRequest, IEnumerable<GetAllFeatureQueryResponse>>
    {
        private readonly IMongoFeatureReadRepository _readRepository;
        private readonly ILogger<GetAllFeatureQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllFeatureQueryRequestHandler(IMongoFeatureReadRepository repository, ILogger<GetAllFeatureQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllFeatureQueryResponse>> Handle(GetAllFeatureQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllFeatureQueryResponse>>(result);
        }
    }
}
