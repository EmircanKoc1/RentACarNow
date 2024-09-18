using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetById
{
    public class GetByIdUserQueryRequestHandler : IRequestHandler<GetByIdUserQueryRequest, ResponseWrapper<GetByIdUserQueryResponse>>
    {
        private readonly IMongoUserReadRepository _readRepository;
        private readonly ILogger<GetByIdUserQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdUserQueryResponse> _responseBuilder;

        public GetByIdUserQueryRequestHandler(
            IMongoUserReadRepository readRepository, 
            ILogger<GetByIdUserQueryRequestHandler> logger, 
            IMapper mapper, 
            ResponseBuilder<GetByIdUserQueryResponse> responseBuilder)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public async Task<ResponseWrapper<GetByIdUserQueryResponse>> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
        {
            var responseBuilder = _responseBuilder;

            var entity = await _readRepository.GetByIdAsync(request.UserId);


            if (entity is null)
            {
                return responseBuilder
                    .SetHttpStatusCode(HttpStatusCode.NotFound)
                    .Build();
            }

            var responseData = _mapper.Map<GetByIdUserQueryResponse>(entity);


            return responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();

        }
    }


}
