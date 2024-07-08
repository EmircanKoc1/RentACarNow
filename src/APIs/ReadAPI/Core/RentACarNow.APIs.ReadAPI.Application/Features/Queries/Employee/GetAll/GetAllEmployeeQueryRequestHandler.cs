using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetAll
{
    public class GetAllEmployeeQueryRequestHandler : IRequestHandler<GetAllEmployeeQueryRequest, IEnumerable<GetAllEmployeeQueryResponse>>
    {
        private readonly IMongoEmployeeReadRepository _readRepository;
        private readonly ILogger<GetAllEmployeeQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllEmployeeQueryRequestHandler(IMongoEmployeeReadRepository repository, ILogger<GetAllEmployeeQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllEmployeeQueryResponse>> Handle(GetAllEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetAllAsync(
                paginationParameter: request.PaginationParameter,
                filter: a => true,
                orderingParameter: request.OrderingParameter
            );

            return _mapper.Map<IEnumerable<GetAllEmployeeQueryResponse>>(result);
        }
    }
}
