using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetById
{
    public class GetByIdAdminQueryRequestHandler : IRequestHandler<GetByIdAdminQueryRequest, GetByIdAdminQueryResponse>
    {
        IMongoAdminReadRepository _readRepository;
        IMapper _mapper;
        ILogger<GetByIdAdminQueryRequestHandler> _logger;

        public GetByIdAdminQueryRequestHandler(IMongoAdminReadRepository readRepository, IMapper mapper, ILogger<GetByIdAdminQueryRequestHandler> logger)
        {
            _readRepository = readRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetByIdAdminQueryResponse> Handle(GetByIdAdminQueryRequest request, CancellationToken cancellationToken)
        {

            var admin = await _readRepository.GetByIdAsync(request.Id);


            return _mapper.Map<GetByIdAdminQueryResponse>(admin);


        }
    }
}
