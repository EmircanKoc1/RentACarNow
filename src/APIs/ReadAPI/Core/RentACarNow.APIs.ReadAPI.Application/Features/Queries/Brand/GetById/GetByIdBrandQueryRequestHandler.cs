using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{

    public class GetByIdBrandQueryRequestHandler : IRequestHandler<GetByIdBrandQueryRequest, ResponseWrapper<GetByIdBrandQueryResponse>>
    {
        private readonly IMongoBrandReadRepository _readRepository;
        private readonly ILogger<GetAllBrandQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdBrandQueryResponse> _responseBuilder;
        private readonly ICustomBrandCacheService _cacheService;

        public GetByIdBrandQueryRequestHandler(
            IMongoBrandReadRepository readRepository,
            ILogger<GetAllBrandQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdBrandQueryResponse> responseBuilder,
            ICustomBrandCacheService brandCacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = brandCacheService;
        }

        public async Task<ResponseWrapper<GetByIdBrandQueryResponse>> Handle(GetByIdBrandQueryRequest request, CancellationToken cancellationToken)
        {


            var entity = _cacheService.GetEntity(request.BrandId);

            if (entity is null)
            {
                entity = await _readRepository.GetByIdAsync(request.BrandId);

                if (entity is null)
                    return _responseBuilder
                        .SetHttpStatusCode(HttpStatusCode.NotFound)
                        .Build();

            }

            _cacheService.SetEntity(request.BrandId, entity, TimeSpan.FromMinutes(1));

            var responseData = _mapper.Map<GetByIdBrandQueryResponse>(entity);


            return _responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();


        }
    }

}
