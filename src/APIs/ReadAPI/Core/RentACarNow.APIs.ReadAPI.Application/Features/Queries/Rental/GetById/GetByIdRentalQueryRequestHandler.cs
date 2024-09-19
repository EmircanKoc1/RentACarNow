using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{
    public class GetByIdRentalQueryRequestHandler : IRequestHandler<GetByIdRentalQueryRequest, ResponseWrapper<GetByIdRentalQueryResponse>>
    {
        private readonly IMongoRentalReadRepository _readRepository;
        private readonly ILogger<GetByIdClaimQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdRentalQueryResponse> _responseBuilder;
        private readonly ICustomRentalCacheService _cacheService;
        public GetByIdRentalQueryRequestHandler(
            IMongoRentalReadRepository readRepository,
            ILogger<GetByIdClaimQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdRentalQueryResponse> responseBuilder,
            ICustomRentalCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<GetByIdRentalQueryResponse>> Handle(GetByIdRentalQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = _cacheService.GetEntity(request.RentalId);

            if (entity is null)
            {
                entity = await _readRepository.GetByIdAsync(request.RentalId);

                if (entity is null)
                    return _responseBuilder
                        .SetHttpStatusCode(HttpStatusCode.NotFound)
                        .Build();

            }

            _cacheService.SetEntity(entity.Id, entity, TimeSpan.FromMinutes(1));

            var responseData = _mapper.Map<GetByIdRentalQueryResponse>(entity);


            return _responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();

        }
    }

}
