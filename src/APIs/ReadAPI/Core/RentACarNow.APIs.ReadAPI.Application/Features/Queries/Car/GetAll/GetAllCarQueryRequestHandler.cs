using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll
{
    public class GetAllCarQueryRequestHandler : IRequestHandler<GetAllCarQueryRequest, IEnumerable<GetAllCarQueryResponse>>
    {
        private readonly IMongoCarReadRepository _readRepository;
        private readonly ILogger<GetAllCarQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCarQueryRequestHandler(IMongoCarReadRepository repository, ILogger<GetAllCarQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCarQueryResponse>> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllCarQueryResponse>>(result);
        }
    }
}



