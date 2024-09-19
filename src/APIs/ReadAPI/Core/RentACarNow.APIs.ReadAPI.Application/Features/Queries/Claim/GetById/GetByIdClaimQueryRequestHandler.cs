using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryRequestHandler : IRequestHandler<GetByIdClaimQueryRequest, ResponseWrapper<GetByIdClaimQueryResponse>>
    {
        private readonly IMongoClaimReadRepository _readRepository;
        private readonly ILogger<GetByIdClaimQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdClaimQueryResponse> _responseBuilder;
        private readonly ICustomClaimCacheService _cacheService;
        public GetByIdClaimQueryRequestHandler(
            IMongoClaimReadRepository readRepository,
            ILogger<GetByIdClaimQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdClaimQueryResponse> responseBuilder,
            ICustomClaimCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<GetByIdClaimQueryResponse>> Handle(GetByIdClaimQueryRequest request, CancellationToken cancellationToken)
        {

            var entity = _cacheService.GetEntity(request.ClaimId);

            if (entity is null)
            {
                entity = await _readRepository.GetByIdAsync(request.ClaimId);

                if (entity is null)
                    return _responseBuilder
                        .SetHttpStatusCode(HttpStatusCode.NotFound)
                        .Build();

            }

            _cacheService.SetEntity(entity.Id, entity, TimeSpan.FromMinutes(1));

            var responseData = _mapper.Map<GetByIdClaimQueryResponse>(entity);


            return _responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();




        }
    }

}
