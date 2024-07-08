using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll
{
    public class GetAllBrandQueryRequestHandler : IRequestHandler<GetAllBrandQueryRequest, IEnumerable<GetAllBrandQueryResponse>>
    {
        private readonly IMongoBrandReadRepository _readRepository;
        private readonly ILogger<GetAllBrandQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllBrandQueryRequestHandler(IMongoBrandReadRepository repository, ILogger<GetAllBrandQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllBrandQueryResponse>> Handle(GetAllBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllBrandQueryResponse>>(result);
        }
    }
}
