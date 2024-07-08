using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll
{
    public class GetAllRentalQueryRequestHandler : IRequestHandler<GetAllRentalQueryRequest, IEnumerable<GetAllRentalQueryResponse>>
    {
        private readonly IMongoRentalReadRepository _readRepository;
        private readonly ILogger<GetAllRentalQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllRentalQueryRequestHandler(IMongoRentalReadRepository repository, ILogger<GetAllRentalQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllRentalQueryResponse>> Handle(GetAllRentalQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllRentalQueryResponse>>(result);
        }
    }
}
