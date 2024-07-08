using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Enums.RepositoryEnums;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;

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

            var pagParam = new PaginationParameters(1, 20);


            var result = await _readRepository.GetAllAsync(
                paginationParameters: pagParam,
                filter: a => true,
                direction: OrderedDirection.None,
                field: a => a.Username);



            return _mapper.Map<IEnumerable<GetAllAdminQueryResponse>>(result);

        }
    }

}
