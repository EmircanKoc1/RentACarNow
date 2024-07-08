using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetAll
{
    public class GetAllCustomerQueryRequestHandler : IRequestHandler<GetAllCustomerQueryRequest, IEnumerable<GetAllCustomerQueryResponse>>
    {
        private readonly IMongoCustomerReadRepository _readRepository;
        private readonly ILogger<GetAllCustomerQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCustomerQueryRequestHandler(IMongoCustomerReadRepository repository, ILogger<GetAllCustomerQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCustomerQueryResponse>> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllCustomerQueryResponse>>(result);
        }
    }
}
