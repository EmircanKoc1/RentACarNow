using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using SharpCompress.Common;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryRequestHandler : IRequestHandler<GetByIdClaimQueryRequest, ResponseWrapper<GetByIdClaimQueryResponse>>
    {
        private readonly IMongoClaimReadRepository _readRepository;
        private readonly ILogger<GetByIdClaimQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdClaimQueryResponse> _responseBuilder;

        public GetByIdClaimQueryRequestHandler(
            IMongoClaimReadRepository readRepository, 
            ILogger<GetByIdClaimQueryRequestHandler> logger, 
            IMapper mapper, 
            ResponseBuilder<GetByIdClaimQueryResponse> responseBuilder)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public async Task<ResponseWrapper<GetByIdClaimQueryResponse>> Handle(GetByIdClaimQueryRequest request, CancellationToken cancellationToken)
        {

            var responseBuilder = _responseBuilder;
            
            var entity = await _readRepository.GetByIdAsync(request.ClaimId);


            if (entity is null)
            {
                return responseBuilder
                    .SetHttpStatusCode(HttpStatusCode.NotFound)
                    .Build();
            }

            var responseData = _mapper.Map<GetByIdClaimQueryResponse>(entity);


            return responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();



        }
    }

}
