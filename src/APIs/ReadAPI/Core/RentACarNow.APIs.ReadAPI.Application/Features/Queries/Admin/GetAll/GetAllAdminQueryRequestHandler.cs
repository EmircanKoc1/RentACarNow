using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequestHandler : IRequestHandler<GetAllAdminQueryRequest, IEnumerable<GetAllAdminQueryResponse>>
    {
        private readonly IMongoAdminReadRepository _readRepository;
        private readonly ILogger<GetAllAdminQueryRequestHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllAdminQueryRequestHandler(IMongoAdminReadRepository repository, ILogger<GetAllAdminQueryRequestHandler> logger, IMapper mapper)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllAdminQueryResponse>> Handle(GetAllAdminQueryRequest request, CancellationToken cancellationToken)
        {
            //var pagParam = new PaginationParameter(request.PaginationParameter.PageNumber, request.PaginationParameter.Size);


            var result = await _readRepository.GetAllAsync(
            paginationParameter: request.PaginationParameter,
            filter: a => true,
            orderingParameter: request.OrderingParameter);

            return _mapper.Map<IEnumerable<GetAllAdminQueryResponse>>(result);



        }
    }

}
